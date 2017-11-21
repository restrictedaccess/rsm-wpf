using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ModelBuilder
    {
        public static string STAFF = "staff";
        public static string CLIENT = "client";
        public static IProxyModel GetInstance(string modelName, dynamic jsonObject)
        {
            IProxyModel model = null;

            if (modelName == ModelBuilder.STAFF)
            {
                model = new Staff();
            }

            if (model == null)
            {
                return model;
            }

            model.convert(jsonObject);

            return model;
        }
    }
}
