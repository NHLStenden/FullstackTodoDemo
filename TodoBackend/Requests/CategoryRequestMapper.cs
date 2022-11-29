using AutoMapper;
using TodoBackend.Entities;

namespace TodoBackend.Requests;

public class CategoryRequestMapper : Profile
{
    public CategoryRequestMapper()
    {
        CreateMap<CategoryRequest, Category>();
    }
}