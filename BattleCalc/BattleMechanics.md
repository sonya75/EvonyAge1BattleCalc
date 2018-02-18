# Evony Battle Mechanics
## This only covers bare troop vs troop battle with no hero, wall or tech at the moment

There are two type of troops, ranged troops and melee troops in battle.
### Ranged Troops:-

> Archer, Ballista, Catapult

### Melee Troops:-

> Cavalry, Cataphract, Worker, Warrior, Sword, Pike, Scout, Transporters, Ram

The whole battle consists of rounds and it continues till it reaches 100 rounds or one side is dead. A round consists of two parts:- **movement** and then after all movement actions are complete, **attack**.

The battlefield size is calculated using the following formula:-

> field_size=max(200+max(range_of_all_ranged_troops),max(range+speed of all melee troops except cavalry, cataphract))

The defender troops start at position 0, in the battle field and the attacker troops start at the end of the battle field, i.e. at position field_size.

### Movement:
There are some rules for troop movement:-
* All the troops from both side move using the following movement order:- 
> Cavalry, Cataphract, Pike, Sword, Warrior, Archer, Ballista, Ram, Catapult, Worker, Transporter, Scout
* If troops of same type are there from both defender and attacker side, then defender troop moves first, then attacker
* If a troop has another troop in range, it won't move
* Ranged troops move till they have another troop in range with the maximum distance they can move being their speed.
* Melee troops move until they reach another troop with the maximum distance they can move being their speed

### Attack:
For this part, all the troops attack each other at the same time, i.e. the damage from all the troops attacking each other is calculated and only subtracted from the troop count after the attacks are all complete.

So, for a troop to attack another troop, there are two parts in this process:-

#### Target Choice:
Here are the rules for target choice:-
* If the attacking troop is a ranged troop:-
	 * If there is another ranged troop from the opposing side in its range, it will target the ranged troop in its range with maximum total attack value
	 * If there is no ranged troop in its range, then it will attack the melee troop with highest speed
 * Otherwise, if the attacking troop is a melee troop, it will target the troop with highest attack value

#### Damage Calculation:
This are the rules for damage calculation:-
* If the attacking troop is a ranged troop:-
	> Damage = ceil(attacker_attack * attacker_count * defender_defense_effect * damage_modifier / defender_life_value)
	
	In the above formula ceil means rounding up the number.
	Defender defense effect is calculated with the formula
	> defense_effect = (1000-defense_value)/1000
	
	The damage modifier in this case depends on the distance of the targeted troop from the attacking troop
	> if (distance <= targeted_troop_range and targeted_troop_is_melee) damage_modifier = 0.25
	> if (distance > attacker_range/2) damage_modifier=0.5
	> otherwise damage_modifier=1
	
* If the attacking troop is a melee troop, the damage calculation is similar to before, i.e.
	> Damage = attacker_attack * attacker_count * defender_defense_effect * damage_modifier / defender_life_value

	But in this case, the damage modifier is taken from the following table instead(if the value isn't present here, means its 1):-
	
|  | WO | W | SC | P | SW | A | T | C | CATA | B | R | CP
|--|--|--|--|--|--|--|--|--|--|--|--|--
|WO|
|W|
|SC|
|P|0|0|0|0|0|0|0|1.8|1.8
|SW|0|0|0|1.1
|A|
|T|
|C|0|0|0|0|0|1.2
|CATA|0|0|0|0|0|1.2
|B|
|R|
|CP|

* Once all the damages are calculated, they are added up for each troop and the total value is rounded down and subtracted from the corresponding troop count.

### The attack, defense, range, speed values can be taken from the game, except in age 1 the worker defense stat is wrong, it shows 10, but it should be 50. Also the sword range and speed is wrong(still undecided).

#### In all calculation, wherever floating point is used, it has precision 15 bit, i.e same as the "float" type in C++, not the type "double"