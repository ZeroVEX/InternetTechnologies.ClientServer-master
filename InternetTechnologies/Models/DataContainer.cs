using System;

namespace InternetTechnologies.DomainModels.Models
{
    [Serializable]
    public class DataContainer<T>
    {
        public T Data { get; set; }

        public OperationType Operation { get; set; }
    }
}
