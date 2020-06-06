using System;

namespace Webao
{
    public interface IRequest
    {
        IRequest BaseUrl(string host);
        IRequest AddParameter(string arg, string val);
        object Get(string path, Type targetType);
        IRequest SetPage(string pageNumber);
        IRequest SetLimit(string limit);
    }
}