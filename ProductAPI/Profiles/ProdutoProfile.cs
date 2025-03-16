using AutoMapper;
using ProductAPI.Data.DTO;
using ProductAPI.Models;

namespace ProductAPI.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<CreateProdutoDTO, Produto>();
        CreateMap<UpdateProdutoDTO, Produto>();
        CreateMap<Produto, UpdateProdutoDTO>();
        CreateMap<Produto, ReadProdutoDTO>();
    }
}
