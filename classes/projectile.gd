extends Area2D
class_name projectile


# PROPERTIES
var Age: Timer = Timer.new()

@export_group("Raw Stats")
@export var Damage: float

## In px/sec
@export var Speed: float = 1

@export var ArmorPiercing: float

@export_range(-1, 5) var Piercing: int = 0

## 0, 0 is the top-left of the screen
@export var Facing: Vector2 = Vector2(-1, 0)

## In Seconds
@export var Duration: int = 1

func _init(damage: float, speed: float, armorpiercing: float, piercing: int, facing: Vector2, duration: int):
	Damage = damage
	Speed = speed
	ArmorPiercing = armorpiercing
	Piercing = piercing
	Facing = facing.normalized()
	Duration = duration

func _ready():
	add_child(Age)
	Age.one_shot = true
	Age.timeout.connect(_end_duration)
	Age.start(Duration)

func _process(delta):
	Facing = Facing.normalized()
	self.position += Facing * Speed

func _end_duration():
	queue_free()
