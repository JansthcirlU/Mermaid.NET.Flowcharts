namespace Mermaid.Flowcharts.Styling;

public interface IStyleClassComponent
{
    string ToMermaidString();
}
public interface IStyleClassComponent<TStyleClassComponent> : IStyleClassComponent, IEquatable<TStyleClassComponent>
{

}
