namespace WebPersonal.BackEnd.API.Filters
{
    public class CustomAttribute
    {
        public readonly bool ContainsAttribute;
        public readonly bool Mandatory;

        public CustomAttribute(bool containsAttribute, bool mandatory)
        {
            ContainsAttribute = containsAttribute;
            Mandatory = mandatory;
        }
    }
}