using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Validate
{
    public static class DataAnnotationHelper
    {

        /// <summary>
        /// 验证model字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="only1Level"></param>
        /// <returns></returns>
        public static string IsValidReStr<T>(this T o, bool only1Level = false)
        {
            StringBuilder strError = new StringBuilder(128);
            List<BrokenRule> ErrorMessageList = IsValid(o, only1Level);
            if (ErrorMessageList == null || ErrorMessageList.Count < 1)
                return strError.ToString();

            foreach (BrokenRule model in ErrorMessageList)
            {
                strError.Append(string.IsNullOrEmpty(strError.ToString()) ? model.Message : model.Message + "<br/>");
            }

            return strError.ToString();
        }
        

        /// <summary>
        /// 验证model字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="only1Level"></param>
        /// <returns></returns>
        public static List<BrokenRule> IsValid<T>(this T o, bool only1Level = false)
        {
            return IsValid(typeof(T), o, only1Level);
        }

        private static List<BrokenRule> IsValid(Type t, object o, bool only1Level)
        {
            List<BrokenRule> errors = new List<BrokenRule>();

            var descriptor = GetTypeDescriptor(t);
            var Properties = descriptor.GetProperties();

            foreach (PropertyDescriptor propertyDescriptor in descriptor.GetProperties())
            {
                foreach (var validationAttribute in propertyDescriptor.Attributes.OfType<ValidationAttribute>())
                {
                    if (!validationAttribute.IsValid(propertyDescriptor.GetValue(o)))
                    {
                        BrokenRule error = new BrokenRule();
                        error.Key = propertyDescriptor.Name;
                        error.Message = validationAttribute.FormatErrorMessage(propertyDescriptor.Name);
                        errors.Add(error);
                    }
                }
            }

            if (!only1Level)
            {
                if (o.GetType().IsClass && !o.GetType().Equals(typeof(string)))
                {
                    foreach (var p in o.GetType().GetProperties())
                    {
                        object pValue = p.GetValue(o, null);
                        if (pValue != null)
                        {
                            List<BrokenRule> pErrors = IsValid(p.PropertyType, pValue, only1Level);
                            errors.AddRange(pErrors);
                        }
                    }
                }
            }

            return errors;
        }
        private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
        }
    }
    public class BrokenRule
    {
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
