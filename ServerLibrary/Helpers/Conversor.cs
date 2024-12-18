namespace ServerLibrary.Helpers;

public class Conversor
{
    public static TDto ConvertToDto<TEntity, TDto>(TEntity entity)
    {
        var dto = Activator.CreateInstance<TDto>();
        foreach (var prop in typeof(TEntity).GetProperties())
        {
            var dtoProp = typeof(TDto).GetProperty(prop.Name);
            if (dtoProp != null && dtoProp.CanWrite)
            {
                dtoProp.SetValue(dto, prop.GetValue(entity));
            }
        }
        return dto;
    }

    public static TEntity ConvertToEntity<TDto, TEntity>(TDto dto, TEntity entity)
    {
        foreach (var prop in typeof(TDto).GetProperties())
        {
            var entityProp = typeof(TEntity).GetProperty(prop.Name);
            if (entityProp != null && entityProp.CanWrite)
            {
                var value = prop.GetValue(dto);
                if (value != null)
                {
                    entityProp.SetValue(entity, value);
                }
            }
        }
        return entity;
    }
}