class_name entity extends Area2D


# PROPERTIES

## RAW STATS
@export_group("Raw Stats")

## Maximum value entity's health can be
@export var MaxHealth: float = 10
@export var Health: float = 10

## Health Regen in Health Regenerated / 10 seconds
@export var HealthRegen: float = 0

## Damage reduction per point of damage - armor efficiency is 35%
@export var Armor: float = 0
const ArmorRate: float = 0.35

## Speed in Moves / Sec
@export var Speed: float = 0.75

## Move Distance in pixels
const MoveDistance: int = 16

@export var AttackDamage: float = 1
@export var Projectiles: float = 1
@export_range(0.125, 1, 0.125) var ProjectileArcVariance: float = 0.25

@export var ProjectileSprite: Texture2D

@export var ProjectileSpeed: float = 1
@export var ProjectileDuration: float = 2
@export var ProjectilePierce: int = 0
@export var ProjectileSize: float = 1


## MULTIPLIERS
@export_group("Multiplier Stats")
@export var MaxHealthMultiplier: float = 1
## Don't use
@export var HealthMultiplier: float = 1
@export var HealthRegenMultiplier: float = 1
@export var ArmorMultiplier: float = 1
@export var ArmorRateMultiplier: float = 1
@export var SpeedMultiplier: float = 1
## Don't use
@export var MoveDistanceMultiplier: float = 1
@export var AttackDamageMultiplier: float = 1
@export var ProjectileMultiplier: float = 1
## Don't use
@export var ProjectileArcVarianceMultiplier: float = 1
@export var ProjectileSpeedMultiplier: float = 1
@export var ProjectileDurationMultiplier: float = 1
@export var ProjectilePierceMultiplier: float = 1
@export var ProjectileSizeMultiplier: float = 1

## ENUMS
enum AttackPosition {IN_FRONT, ENTITY_LOCATION, BEHIND, RANDOM, ARBITRARY}
enum Direction {NORTH, SOUTH, EAST, WEST}


func _ready():
	$AnimatedSprite2D.play("character_idle")

func Move(direction: Direction) -> bool:
	var tween: Tween = create_tween()
	tween.set_trans(Tween.TRANS_SINE)
	match direction:
		Direction.NORTH:
			tween.tween_property(self, "position", (Vector2.UP * MoveDistance) + position, 0.1 / (Speed * SpeedMultiplier))
			# self.position += Vector2(0, -1 * MoveDistance * MoveDistanceMultiplier)
		Direction.SOUTH:
			tween.tween_property(self, "position", (Vector2.DOWN * MoveDistance) + position, 0.1 / (Speed * SpeedMultiplier))
			# self.position += Vector2(0, 1 * MoveDistance * MoveDistanceMultiplier)
		Direction.EAST:
			tween.tween_property(self, "position", (Vector2.RIGHT * MoveDistance) + position, 0.1 / (Speed * SpeedMultiplier))
			# self.position += Vector2(1 * MoveDistance * MoveDistanceMultiplier, 0)
		Direction.WEST:
			tween.tween_property(self, "position", (Vector2.LEFT * MoveDistance) + position, 0.1 / (Speed * SpeedMultiplier))
			# self.position += Vector2(-1 * MoveDistance * MoveDistanceMultiplier, 0)
	Attack(direction)
	return true

func Attack(attackDirection: Direction, attackPosition: AttackPosition = AttackPosition.IN_FRONT) -> bool:
	var roundedProjectiles: int = Projectiles
	if (randf() < (Projectiles - roundedProjectiles)): roundedProjectiles += 1
	
	var projcount: int = 0
	for i in range(roundedProjectiles):
		var proj: projectile = SpawnProjectile(attackDirection, attackPosition)
		match attackDirection:
			Direction.NORTH:
				proj.Facing = Vector2.UP
			Direction.SOUTH:
				proj.Facing = Vector2.DOWN
			Direction.EAST:
				proj.Facing = Vector2.RIGHT
			Direction.WEST:
				proj.Facing = Vector2.LEFT
		
		match roundedProjectiles % 2:
			0:
				# Even proj count code
				proj.Facing = Vector2.from_angle(proj.Facing.angle() + ((ProjectileArcVariance * ProjectileArcVarianceMultiplier) * (projcount - int(roundedProjectiles / 2)) + ((ProjectileArcVariance * ProjectileArcVarianceMultiplier) / 2)))
				pass
			1:
				# Odd proj count code
				proj.Facing = Vector2.from_angle(proj.Facing.angle() + ((ProjectileArcVariance * ProjectileArcVarianceMultiplier) * (projcount - int(roundedProjectiles / 2))))
				pass
		
		match attackPosition:
			AttackPosition.IN_FRONT:
				proj.position += proj.Facing
		proj.rotation = proj.get_angle_to(position)
		projcount += 1
	
	return false

func SpawnProjectile(attackDirection: Direction, attackPosition: AttackPosition = AttackPosition.IN_FRONT) -> projectile:
	var proj: projectile = projectile.new(AttackDamage * AttackDamageMultiplier, ProjectileSpeed * ProjectileSpeedMultiplier, 0, ProjectilePierce * ProjectilePierceMultiplier, Vector2.ZERO, ProjectileDuration * ProjectileDurationMultiplier)
	var sprit: Sprite2D = Sprite2D.new()
	sprit.texture = ProjectileSprite
	var shap: CollisionShape2D = CollisionShape2D.new()
	var rect: RectangleShape2D = RectangleShape2D.new()
	rect.set_size(Vector2(5 * ProjectileSize * ProjectileSizeMultiplier, 5 * ProjectileSize * ProjectileSizeMultiplier))
	shap.shape = rect
	proj.add_child(sprit)
	proj.add_child(shap)
	add_sibling(proj)
	proj.position = position
	return proj
