namespace Tutorials {
    public interface ITutorialCondition {
        bool IsMet();
    }
    public class DisassembledCondition : ITutorialCondition {
        public CarPart Part { get; private set; }
        public DisassembledCondition(CarPart part) {
            Part = part;
        }
        public bool IsMet() {
            return Part.disassembled == true;
        }
    }
}