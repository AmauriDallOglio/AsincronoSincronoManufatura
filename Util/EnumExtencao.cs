using System.ComponentModel;
using System.Reflection;

namespace AsincronoSincronoManufatura.Util
{
    public static class EnumExtencao
    {
        public static string ObterDescricao(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }

            return value.ToString(); // Retorna o nome do enum se nenhuma descrição for encontrada
        }
    }
}
