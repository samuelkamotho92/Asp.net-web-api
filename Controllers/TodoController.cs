using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;
using Todo_App.Models;

namespace Todo_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        //import our Response DTO

        private ResponseDto _responseDto;
        //Interface of Imapper 
        private readonly IMapper _mapper;
        //instance of DTO

        public TodoController(IMapper mapper)
        {
            _responseDto = new ResponseDto();
            //save instant created to _mapper;
            _mapper = mapper;
        }
        private static List<Todo> Todos = new List<Todo>()
        {
            new Todo()
            {
                Name = "Asp.Net backend",
                Description = "Introduction in Asp.Net Web Api",
                CreatedDate = DateTime.Now,
                DeadlineDate = new DateTime(2023, 12, 31)
            }
        };

        //Add Todo application

        [HttpGet]
        public ActionResult<ResponseDto> getAllTodos()
        {
            _responseDto.Result = Todos;
            _responseDto.Message = "Success";
            Console.WriteLine(_responseDto.StatusCode);
            //Add to database
            return Ok(_responseDto);
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseDto> getOneTodos(Guid id) {

         var todo = Todos.Find(x => x.Id == id);
            if(todo != null)
            {
                _responseDto.Result = todo;
                _responseDto.Message = "Success";
                return Ok(_responseDto);
            }
            _responseDto.Result = null;
            _responseDto.StatusCode = HttpStatusCode.NotFound;
            return NotFound(_responseDto);
        }

        //Add a todo
        [HttpPost]
        public ActionResult<ResponseDto> createTodo(AddTodoDto addTodoDto)
        {
            //Mapping to Todo format
            var newTodo =  _mapper.Map<Todo>(addTodoDto);
            _responseDto.Result = newTodo;
            _responseDto.Message = "Success todo added";
            _responseDto.StatusCode = HttpStatusCode.Created;
            Todos.Add(newTodo);

            return Created($"api/todo/{newTodo.Id}",_responseDto);
        }

        //Update Todo List

        [HttpPut]
        public ActionResult<ResponseDto> updateDto(Guid id,AddTodoDto updatedTodo)
        {
            //get specific todo to update
            var todo = Todos.Find(x=>x.Id == id);
            if(todo != null)
            {
                //update the todo
                _mapper.Map(updatedTodo,todo);
                _responseDto.Message = "Successfully updated todo";
                _responseDto.Result = updatedTodo;
                return Ok(_responseDto);
            }
            _responseDto.Message = "Todo with Id not found";
            _responseDto.StatusCode = HttpStatusCode.NotFound;
            return NotFound(_responseDto);
        }

        //Delete

        [HttpDelete]
        public ActionResult<ResponseDto> deleteDto(Guid id,AddTodoDto deleteTodod)
        {
            //Get todo
            var todo = Todos.Find(x=>x.Id == id);
            if(todo != null)
            {
                //delete the todo
                Todos.Remove(todo);
                _responseDto.Message = "Todo removed successfully";
                _responseDto.StatusCode = HttpStatusCode.NotFound;
                _responseDto.Result = null;
                return Ok(_responseDto);
            }
            _responseDto.StatusCode = HttpStatusCode.NotFound;
            return NotFound(_responseDto);
        }

    }
}
