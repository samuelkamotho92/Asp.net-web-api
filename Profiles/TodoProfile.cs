using AutoMapper;
using Todo_App.Models;

namespace Todo_App.Profiles
{
    public class TodoProfile:Profile
    {
        public TodoProfile() {

            CreateMap<AddTodoDto, Todo>().ReverseMap();

        }
    }
}
