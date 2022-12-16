using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TarefaController : ControllerBase
  {
    private readonly OrganizadorContext _context;

    public TarefaController(OrganizadorContext context)
    {
      _context = context;
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
      // DONE: Buscar o Id no banco utilizando o EF
      var data = _context.Tarefas.Find(id);
      // DONE: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
      if (data == null) { return new NotFoundResult(); }

      // caso contrário retornar OK com a tarefa encontrada
      return Ok(data);
    }

    [HttpGet("ObterTodos")]
    public IActionResult ObterTodos()
    {
      // DONE: Buscar todas as tarefas no banco utilizando o EF
      var data = _context.Tarefas.Select(x => x);
      return Ok(data);
    }

    [HttpGet("ObterPorTitulo")]
    public IActionResult ObterPorTitulo(string titulo)
    {
      // DONE: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
      var data = _context.Tarefas.Where(x => x.Titulo == titulo);
      // Dica: Usar como exemplo o endpoint ObterPorData
      return Ok(data);
    }

    [HttpGet("ObterPorData")]
    public IActionResult ObterPorData(DateTime data)
    {
      var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
      return Ok(tarefa);
    }

    [HttpGet("ObterPorStatus")]
    public IActionResult ObterPorStatus(EnumStatusTarefa status)
    {
      // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
      // Dica: Usar como exemplo o endpoint ObterPorData
      var tarefa = _context.Tarefas.Where(x => x.Status == status);
      return Ok(tarefa);
    }

    [HttpPost]
    public IActionResult Criar(Tarefa tarefa)
    {
      if (tarefa.Data == DateTime.MinValue)
        return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

      // DONE: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
      var tarefaData = _context.Tarefas.Add(tarefa);
      _context.SaveChanges();

      return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Tarefa tarefa)
    {
      var tarefaBanco = _context.Tarefas.Find(id);

      if (tarefaBanco == null)
        return NotFound();

      if (tarefa.Data == DateTime.MinValue)
        return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

      // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
      // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
      var tarefaBanco = _context.Tarefas.Find(id);

      if (tarefaBanco == null)
        return NotFound();

      // DONE: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
      _context.Remove(tarefaBanco);
      _context.SaveChanges();

      return NoContent();
    }
  }
}
