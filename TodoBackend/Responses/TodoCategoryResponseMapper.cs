using AutoMapper;
using TodoBackend.Entities;

namespace TodoBackend.Responses;

public class TodoCategoryResponseMapper : Profile
{
    public TodoCategoryResponseMapper()
    {
        CreateMap<Todo, TodoCategoryResponse>()
            .ForMember(x => x.Id, x =>
                x.MapFrom(c => c.TodoId))
            .ForMember(x => x.Completed, x =>
                x.MapFrom(w => w.Completed ? "Klaar" : "Nog te doen"))
            .ForMember(x => x.Category, x =>
                x.MapFrom(w => w.Category.Description))
            .ForMember(x => x.Slug, x =>
                x.MapFrom(w => w.Category.Slug));
    }
}