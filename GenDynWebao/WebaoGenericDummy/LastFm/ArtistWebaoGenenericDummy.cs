using System.Collections.Generic;
using DynWebao;
using GenDynWebao.IGenericWebaos;
using GenericWebao.GenericDynBuilder;
using Webao;
using Webao.Test.Dto.LastFm;

namespace GenDynWebao.WebaoGenericDummy.LastFm
{
    public class ArtistWebaoGenenericDummy : Base, IWebaoGenericArtist
    {
        public ArtistWebaoGenenericDummy(IRequest req) : base(req)
        {
        }


        public Artist GetInfo(string name)
        {
            var path = "?method=artist.getinfo&artist={name}";
            //var test = new DelegateGeneratedIL();
            //test.Mapping<DtoArtist>(dto => dto.GetArtist());
            //MethodInformation.Delegates.Add(new Delegate("target","target"));
            var dto1 = (DtoArtist) req.Get(CompletePath(path, new[] {name}), typeof(DtoArtist));
            return (Artist) MethodInformation.Delegates[0].DynamicInvoke(dto1);
        }

        public List<Artist> Search(string name, int page)
        {
            var path = "?method=artist.search&artist={name}&page={page}";
            //var test = new DelegateGeneratedIL();
            //test.Mapping<DtoSearch>(dto => dto.GetArtistList());
            var dto2 = (DtoSearch) req.Get(CompletePath(path, new[] {name, page.ToString()}), typeof(DtoSearch));
            return (List<Artist>) MethodInformation.Delegates[15].DynamicInvoke(dto2);
        }
    }
}