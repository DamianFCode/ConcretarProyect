using Newtonsoft.Json;

namespace Concretar.Helper
{
    public static  class SandboxHelper
    {
        public enum TypeModel : int
        {
            CARD_VALID = 1,
            CARD_INVALID = 2,
            CARD_VALID_REFUSE_VALID = 3,
            CARD_VALID_REFUSE_INVALID = 4
        }

        public enum COPlataformaSandbox
        {
            CARD_VALID_REFUSE_VALID = 111111,
            CARD_VALID_REFUSE_INVALID = 222222
        }


        public static bool DeepCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                if (!objValue.Equals(anotherValue)) result = false;
            }

            return result;
        }

        public static bool JsonCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);

            return objJson == anotherJson;
        }
    }
}
