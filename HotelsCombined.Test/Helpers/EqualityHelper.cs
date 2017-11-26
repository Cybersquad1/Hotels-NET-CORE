using System.Collections;
using System.Linq;

namespace Shared.Helper.Test
{
    public static class EqualityHelper
    {
        /* <Summary>
         *  Compares whether 2 objects are equal in terms of property values
         * </Summary>
         * <Param>Actual object to test</Param>
         * <Param>Object to test against</Param>
         * <Param>List of properties to ignore in comparison</Param>
         * <Returns>Bool value indicating whether objects are equal or not</Returns>
        */
        public static bool PropertyValuesAreEqual(object actual, object expected, string[] ignoreList)
        {
            //Get properties of object to compare against
            var properties = expected.GetType().GetProperties();
            //Loop through each property to compare
            foreach (var property in properties.Where(x => !ignoreList.Contains(x.Name)))
            {
                var expectedValue = property.GetValue(expected, null);
                var actualValue = property.GetValue(actual, null);
                var list = actualValue as IList;

                //If property value is a list
                if (list != null)
                {
                    //Compare lists
                    if(!AssertListsAreEqual(list, (IList)expectedValue))
                    {
                        return false;
                    }
                }
                else if (!Equals(expectedValue, actualValue))
                {
                    return false;
                }   
            }

            return true;
        }

        /* <Summary>
        *  Compares lists for equality in terms of lenght and content
        * </Summary>
        * <Param>Actual list to test</Param>
        * <Param>List to test against</Param>
        * <Returns>Bool value indicating whether lists are equal or not</Returns>
       */
        private static bool AssertListsAreEqual(IList actualList, IList expectedList)
        {
           
            if (actualList.Count != expectedList.Count)
            {
                return false;
            }

            for (var i = 0; i < actualList.Count; i++)
            {
                //If compare objects for equalit in terms of properties
                if (!PropertyValuesAreEqual(actualList[i], expectedList[i], new string[0]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}