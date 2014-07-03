using System;

namespace Graffo.Entidades
{
    public class Data : Entidade
    {
        public virtual DateTime Date { get; set; }
        public virtual TrelloDataType DataType { get; set; }
        public virtual String Json { get; set; }
        public virtual int DataParentId { get; set; }
        public virtual string TrelloId { get; set; }
    }
}
