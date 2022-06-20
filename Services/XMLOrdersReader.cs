﻿using CPSDevExerciseWeb.Models;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CPSDevExerciseWeb.Services
{
    [XmlRoot("BigShoeDataImport")]
    public class DocumentElement : List<Order>
    {
    }
    public class XMLOrdersReader : IOrdersReader
    {
        private readonly IConfiguration _configuration;
        public XMLOrdersReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ImportOrderPackage ReadOrders(string inputSource)
        {
            var retObj = new ImportOrderPackage();
            retObj.ValidationErrors = new List<string>();

            if (!ValidateOrdersXML(inputSource, retObj.ValidationErrors))
            {
                return retObj;
            }

            var serializer = new XmlSerializer(typeof(DocumentElement));
            DocumentElement? ordersDoc = null;
            using (var reader = XmlReader.Create(new StringReader(inputSource)))
            {
                ordersDoc = serializer.Deserialize(reader) as DocumentElement;
            }

            retObj.Orders = ordersDoc != null ? ordersDoc.ToArray() : new Order[] { };

            for (int i=0;i< retObj.Orders.Length; i++)
            {
                var order = retObj.Orders[i];
                //additional validation
                var err = ValidateOrderCustom(i, order);
                if (!string.IsNullOrEmpty(err))
                {
                    retObj.ValidationErrors.Add(err);
                }
            }

            return retObj;
        }

        bool ValidateOrdersXML(string inputSource, List<string> erros)
        {
            bool errorsExist = false;
            inputSource = ("" + inputSource).Trim();

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", _configuration.GetSection("Orders")["xsd_file"]);

            XDocument ordersXMLDoc = XDocument.Load(new StringReader(inputSource));

            ordersXMLDoc.Validate(schemas, (o, e) =>
            {
                erros.Add(e.Message);
                errorsExist = true;
            });

            return errorsExist;
        }

        Regex emailregex = new Regex("^\\S+@\\S+\\.\\S+$");

        string ValidateOrderCustom(int index, Order order)
        {
            StringBuilder err = new StringBuilder();

            if (string.IsNullOrWhiteSpace(order.CustomerName))
            {
                err.AppendFormat("Order[{0}]: CustomerName required", index);
            }

            if (string.IsNullOrWhiteSpace(order.CustomerEmail))
            {
                err.AppendFormat("Order[{0}]: CustomerEmail required", index);
            }

            if (!emailregex.IsMatch(order.CustomerEmail))
            {
                err.AppendFormat("Order[{0}]: CustomerEmail is not a valid email", index);
            }

            if (order.Quantity % 1000 != 0)
            {
                err.AppendFormat("Order[{0}]: Quantity must be in multiples of 1000", index);
            }

            if (order.Quantity % 1000 != 0)
            {
                err.AppendFormat("Order[{0}]: Quantity must be in multiples of 1000", index);
            }

            if(order.Size < (decimal)11.5 || order.Size> 15 || (order.Size * 10) % 5 != 0)
            {
                err.AppendFormat("Order[{0}]: Size must be 11.5 to 15 including half sizes", index);
            }

            DateTime dateLimit = DateTime.Now.Date;
            int days = 10;
            while (days > 0)
            {
                dateLimit.AddDays(1);

                if (dateLimit.DayOfWeek != DayOfWeek.Sunday || dateLimit.DayOfWeek != DayOfWeek.Saturday)
                {
                    days--;
                }
            }

            if (order.DateRequired.Date < dateLimit)
            {
                err.AppendFormat("Order[{0}]: Date must be valid and at least 10 working days into the future", index);

            }

            return err.ToString();
        }
    }
}
