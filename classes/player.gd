class_name player extends entity


# PROPERTIES

## RAW STATS
@export_group("Raw Stats")


func Move(direction: Direction) -> bool:
	match direction:
		Direction.WEST:
			$AnimatedSprite2D.set_flip_h(false)
		Direction.EAST:
			$AnimatedSprite2D.set_flip_h(true)
	return super(direction)

func _ready():
	super()
	Game.input_player_move_north.connect(Move.bind(Direction.NORTH))
	Game.input_player_move_south.connect(Move.bind(Direction.SOUTH))
	Game.input_player_move_east.connect(Move.bind(Direction.EAST))
	Game.input_player_move_west.connect(Move.bind(Direction.WEST))


#func Attack(attackPosition: AttackPosition = AttackPosition.IN_FRONT) -> bool:
#	super(attackPosition)
#	return false


