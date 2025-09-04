using Cysharp.Threading.Tasks;

public interface IAssemblyPart {
    public UniTask StartAssemble();

    public UniTask StartDisassemble();
}