

public struct IntVector2 {
	public int x,y;
	public override int GetHashCode () {
		return x*971 + y*937;
	}
	public override bool Equals (object obj) {
		if(!obj.GetType().Equals(typeof(IntVector2))) {
			return false;
		}
		else {
			IntVector2 v = (IntVector2)obj;
			return x == v.x && y == v.y;
		}
	}
	public IntVector2(int i, int j) {
		this.x = i;
		this.y = j;
	}
}