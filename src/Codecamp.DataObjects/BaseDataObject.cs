using MvvmHelpers;
using System;

namespace Codecamp.Mobile.DataObjects
{
    public class BaseDataObject : ObservableObject, IBaseDataObject
    {
        public Guid Id { get; set; }
    }
}