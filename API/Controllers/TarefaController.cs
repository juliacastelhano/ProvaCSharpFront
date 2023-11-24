using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers;

[Route("api/tarefa")]
[ApiController]
public class TarefaController : ControllerBase
{
	private readonly AppDataContext _context;

	public TarefaController(AppDataContext context) =>
		_context = context;

	// GET: api/tarefa/listar
	[HttpGet]
	[Route("listar")]
	public IActionResult Listar()
	{
		try
		{
			List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria).ToList();
			return Ok(tarefas);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}

	// POST: api/tarefa/cadastrar
	[HttpPost]
	[Route("cadastrar")]
	public IActionResult Cadastrar([FromBody] Tarefa tarefa)
	{
		try
		{
			Categoria? categoria = _context.Categorias.Find(tarefa.CategoriaId);
			if (categoria == null)
			{
				return NotFound();
			}
			tarefa.Categoria = categoria;
			_context.Tarefas.Add(tarefa);
			_context.SaveChanges();
			return Created("", tarefa);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	
	[HttpGet]
	[Route("buscar/{id}")]
	public IActionResult Buscar([FromRoute] int id)
	{
		try
		{
			Tarefa? tarefaCadastrada = _context.Tarefas.FirstOrDefault(x => x.TarefaId == id);
			if (tarefaCadastrada != null)
			{
				return Ok(tarefaCadastrada);
			}
			return NotFound();
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	
	[HttpPatch]
	[Route("alterar/{id}")]
	// public IActionResult Alterar([FromRoute] int id,
	// 	[FromBody] Tarefa tarefa)
		public IActionResult Alterar([FromRoute] int id)
	{
		try
		{
			Tarefa? tarefaCadastrada =
				_context.Tarefas.FirstOrDefault(x => x.TarefaId == id);
			if (tarefaCadastrada != null && tarefaCadastrada.Status == "Não Iniciada")
			{
				Categoria? categoria =
					_context.Categorias.Find(tarefaCadastrada.CategoriaId);
				if (categoria == null)
				{
					return NotFound();
				}
				// tarefaCadastrada.Categoria = categoria;
				// tarefaCadastrada.Titulo = tarefa.Titulo;
				// tarefaCadastrada.Descricao = tarefa.Descricao;
				tarefaCadastrada.Status = "Em andamento";

				_context.Tarefas.Update(tarefaCadastrada);
				_context.SaveChanges();
				return Created("", tarefaCadastrada);
				// return Ok();
			}
			else if (tarefaCadastrada != null && tarefaCadastrada.Status == "Em andamento") 
			{
				Categoria? categoria =
					_context.Categorias.Find(tarefaCadastrada.CategoriaId);
				if (categoria == null)
				{
					return NotFound();
				}
				
				// tarefaCadastrada.Categoria = categoria;
				// tarefaCadastrada.Titulo = tarefa.Titulo;
				// tarefaCadastrada.Descricao = tarefa.Descricao;
				tarefaCadastrada.Status = "Concluído";

				_context.Tarefas.Update(tarefaCadastrada);
				_context.SaveChanges();
				// return Ok();
				return Created("", tarefaCadastrada);
			}
			else if (tarefaCadastrada != null && tarefaCadastrada.Status == "Concluído")
			{
				Categoria? categoria =
					_context.Categorias.Find(tarefaCadastrada.CategoriaId);
				if (categoria == null)
				{
					return NotFound();
				}
				
				// tarefaCadastrada.Categoria = categoria;
				// tarefaCadastrada.Titulo = tarefa.Titulo;
				// tarefaCadastrada.Descricao = tarefa.Descricao;
				tarefaCadastrada.Status = "Concluído";

				_context.Tarefas.Update(tarefaCadastrada);
				_context.SaveChanges();
				return Created("", tarefaCadastrada);
			}
			return NotFound();
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	
	[HttpGet]
	[Route("listarNaoConcluidas")]
	public IActionResult ListarNaoConcluidas()
	{
		try
		{
			
			List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria)
			.Where(x => x.Status == "Em andamento" || x.Status == "Não Iniciada")
			.ToList();
			
			// List<Tarefa> tarefas = _context.Tarefas.Where(x => x.Status == "Não Iniciada")
			// .ToList();
			
			// List<Pedido> pedidos =
			// _ctx.Pedidos
			// .Include(x => x.Atendente)
			// .Include(x => x.Cliente)
			// .Include(x => x.Cardapio)
			// .Include(x => x.Carrinho)
			// // .Include(x => x.Carrinho.Cliente)
			// .ToList();
			
			return Ok(tarefas);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
	
	[HttpGet]
	[Route("listarConcluidas")]
	public IActionResult ListarConcluidas()
	{
		try
		{
			
			List<Tarefa> tarefas = _context.Tarefas.Include(x => x.Categoria)
			.Where(x => x.Status == "Concluído")
			.ToList();
						
			return Ok(tarefas);
		}
		catch (Exception e)
		{
			return BadRequest(e.Message);
		}
	}
}
