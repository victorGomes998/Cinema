using AutoMapper;
using Cinema.Model;

namespace Cinema.Data;

public class EstabelecimentoProfile : Profile
{
    public EstabelecimentoProfile()
    {
        CreateMap<CreateEstabelecimentoDto, Estabelecimento>();
        CreateMap<UpdateEstabelecimentoDto, Estabelecimento>();
        CreateMap<Estabelecimento, UpdateEstabelecimentoDto>();
        CreateMap<Estabelecimento, ReadEstabelecimentoDto>();
    }
}