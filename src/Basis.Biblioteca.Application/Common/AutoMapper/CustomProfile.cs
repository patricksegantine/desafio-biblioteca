using AutoMapper;
using Basis.Biblioteca.Application.Common.DTOs;
using Basis.Biblioteca.Application.UseCases.Livro.Create;
using Basis.Biblioteca.Application.UseCases.Livro.Get;

namespace Basis.Biblioteca.Application.Common.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<Domain.Entities.Livro, CreateLivroResult>();
        CreateMap<Domain.Entities.Livro, GetLivroResult>();
        CreateMap<Domain.Entities.Autor, AutorDto>();
        CreateMap<Domain.Entities.Assunto, AssuntoDto>();
        CreateMap<Domain.Entities.PrecoVenda, PrecoVendaDto>()
            .ReverseMap();

        CreateMap<CreateLivroRequest, Domain.Entities.Livro>()
            .ForMember(dst => dst.Autores, opt => opt.Ignore())
            .ForMember(dst => dst.Assuntos, opt => opt.Ignore());
    }
}
