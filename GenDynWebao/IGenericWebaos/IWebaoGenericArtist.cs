using System.Collections.Generic;
using Webao.Test.Dto.LastFm;

namespace GenDynWebao.IGenericWebaos
{
    public interface IWebaoGenericArtist
    {
        Artist GetInfo(string name);


        List<Artist> Search(string name, int page);
    }
}