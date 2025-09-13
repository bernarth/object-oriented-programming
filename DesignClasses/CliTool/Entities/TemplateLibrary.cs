namespace CliTool.Entities;

public sealed class TemplateLibrary
{
  private readonly Dictionary<string, Template> _templates = [];

  public TemplateLibrary Add(Template template)
  {
    _templates[template.Id] = template;

    return this;
  }

  public bool TryGet(string id, out Template? template)
  {
    return _templates.TryGetValue(id, out template);
  }

  public IEnumerable<Template> All() => _templates.Values.OrderBy(t => t.Id);
}