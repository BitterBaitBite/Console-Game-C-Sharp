namespace Guillermo_Concepcion_Entrega_C_Sharp.Classes {
	abstract class Item {
		public ushort Id { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }

		public int BuyValue { get; protected set; }

		public Item(ushort id, string name, string description, int buyValue) {
			Id = id;
			Name = name;
			Description = description;
			BuyValue = buyValue;
		}

		public override string ToString() {
			return $"\t {Id} \t|\t {Name} \t|\t {Description} \t|\t {BuyValue} \t";
		}
	}
}
