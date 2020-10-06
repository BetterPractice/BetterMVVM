using System;

namespace BetterPractice.BetterMvvm.Navigation
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class PageModelMappingAttribute : Attribute
    {
        public Type PageType { get; }
        public Type PageModelType { get; }

        public PageModelMappingAttribute(Type page, Type pageModel)
        {
            PageType = page;
            PageModelType = pageModel;
        }
    }
}
