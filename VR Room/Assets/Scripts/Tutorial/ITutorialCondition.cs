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
        public override bool Equals(object condition) {
            return condition is DisassembledCondition other &&
           Part.PartInfo == other.Part.PartInfo; ;
        }
        public override int GetHashCode() {
            return Part.PartInfo != null ? Part.PartInfo.GetHashCode() : 0;
        }
    }

    public class AssembledCondition : ITutorialCondition {
        public CarPart Part { get; private set; }
        public AssembledCondition(CarPart part) {
            Part = part;
        }
        public bool IsMet() {
            return Part.disassembled == false;
        }

        public override bool Equals(object condition) {
            return condition is AssembledCondition other &&
           Part.PartInfo == other.Part.PartInfo; ;
        }
        public override int GetHashCode() {
            return Part.PartInfo != null ? Part.PartInfo.GetHashCode() : 0;
        }
    }
}