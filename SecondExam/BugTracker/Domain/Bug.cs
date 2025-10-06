namespace BugTracker.Domain;

public class Bug
{
  // Id ahora es 'init' para asegurar que solo se establece en la construcción del objeto o inicializador.
  public int Id { get; init; }

  // Title ahora tiene 'private set' y se modifica solo a través del método Rename().
  private string _title;
  public string Title
  {
    get => _title;
    private set
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new ArgumentException("Title cannot be null or empty.", nameof(value));
      }
      _title = value;
    }
  }

  public Severity Severity { get; private set; } // Severity se puede cambiar, pero quizás por un método explícito
  public bool IsFixed { get; private set; } = false; // IsFixed se modifica solo a través del método Fix().

  // Constructor con validación para el título
  public Bug(int id, string title, Severity severity)
  {
    // Las validaciones de propiedades se disparan aquí gracias al 'set' privado
    Id = id;
    Title = title;
    Severity = severity;
  }

  // Método para marcar como solucionado
  public void Fix()
  {
    if (IsFixed)
    {
      throw new InvalidBugStateException($"Bug with ID {Id} is already fixed.");
    }
    IsFixed = true;
  }

  // Método para renombrar
  public void Rename(string newTitle)
  {
    if (string.IsNullOrWhiteSpace(newTitle))
    {
      throw new ArgumentException("New title cannot be null or empty.", nameof(newTitle));
    }
    _title = newTitle;
  }

    public void ChangeSeverity(Severity newSeverity)
  {
      Severity = newSeverity;
  }
}