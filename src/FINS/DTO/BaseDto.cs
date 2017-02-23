namespace FINS.DTO
{
    /// <summary>
    /// Base Data transfer DTO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDto<T>
    {
        /// <summary>
        /// Primary Key Id
        /// </summary>
        public T Id { get; set; }
    }
}
