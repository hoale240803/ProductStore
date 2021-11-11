using System;

namespace ProductStore.API.DBFirst.ViewModels
{
    internal class StringValueAttribute : Attribute
    {
        private string v;

        public StringValueAttribute(string v)
        {
            this.v = v;
        }
    }
}