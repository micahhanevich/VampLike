extends Node


# SIGNALS
signal game_paused
signal game_unpaused
signal game_start
signal game_quit
signal game_settings_open
signal game_settings_close

signal game_saving
signal game_saved

signal game_loading
signal game_loaded

signal game_reloading
signal game_reloaded

## INPUT SIGNALS
signal input_player_move_north
signal input_player_move_south
signal input_player_move_east
signal input_player_move_west


# PROPERTIES
var Pausable: bool = false
var Paused: bool = false
var SettingsOpen: bool = false


enum Scenes {MAIN_MENU, TEST_SCENE}


# BUILTINS
func _input(event: InputEvent) -> void:
	
	if event.is_action("player_move_north"):
		emit_signal("input_player_move_north")
	elif event.is_action("player_move_south"):
		emit_signal("input_player_move_south")
	
	if event.is_action("player_move_east"):
		emit_signal("input_player_move_east")
	elif event.is_action("player_move_west"):
		emit_signal("input_player_move_west")
	
	if event.is_action_pressed("game_pause") && Pausable: toggle_pause_game()

func _ready():
	randomize()


func quit_game():
	emit_signal("game_quit")
	get_tree().quit()

func main_menu():
	emit_signal("game_loading")
	get_tree().change_scene_to_file("res://scenes/main_menu.tscn")
	emit_signal("game_loaded")

func reload_scene():
	emit_signal("game_reloading")
	get_tree().reload_current_scene()
	emit_signal("game_reloaded")


func load_scene(scene: String):
	emit_signal("game_loading")
	get_tree().change_scene_to_file(scene)
	emit_signal("game_loaded")

func load_scene_enum(scene: Scenes):
	emit_signal("game_loading")
	
	match scene:
		Scenes.MAIN_MENU:
			get_tree().change_scene_to_file("res://scenes/main_menu.tscn")
		Scenes.TEST_SCENE:
			get_tree().change_scene_to_file("res://scenes/test_scene.tscn")
	
	emit_signal("game_loaded")


func _pause_game():
	emit_signal("game_paused")
	# Game Pause Code

func _unpause_game():
	emit_signal("game_unpaused")
	# Game Unpause Code

func toggle_pause_game():
	if Paused:
		_unpause_game()
	else:
		_pause_game()
