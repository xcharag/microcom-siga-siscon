namespace ClientLibrary.Helpers;

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
    
    public static List<TDto> ConvertToDtoList<TEntity, TDto>(List<TEntity> entities)
    {
        var dtos = new List<TDto>();
        foreach (var entity in entities)
        {
            var dto = ConvertToDto<TEntity, TDto>(entity);
            dtos.Add(dto);
        }
        return dtos;
    }
    
    public static List<TEntity> ConvertToEntityList<TDto, TEntity>(List<TDto> dtos, TEntity entityPara) where TEntity : new()
    {
        var entities = new List<TEntity>();
        foreach (var dto in dtos)
        {
            var newEntitie = new TEntity();
            var entity = ConvertToEntity(dto, entityPara);
            entities.Add(entity);
        }
        return entities;
    }
}