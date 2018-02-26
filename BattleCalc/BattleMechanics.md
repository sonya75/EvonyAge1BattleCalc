
# Evony Battle Mechanics
## These are base stats of different troop types
|  | Attack  | Defense | Life | Speed | Range
|--|--|--|--|--|--|
| Worker |5|50|100|180|10
|Warrior | 50|50|200|200|20
|Scout|20|50|100|3000|20
|Pike|150|150|300|300|50
|Sword|100|250|350|275|30
|Archer|120|50|250|250|1200
|Transporter|10|60|700|150|10
|Cavalry|250|180|500|1000|100
|Cataphract|350|350|1000|750|80
|Ballista|450|160|320|100|1400
|Ram|250|160|5000|120|600
|Catapult|600|200|480|80|1500
|Archer Tower|300|360|2000|0|1300

At the beginning of the battle all troop stats, i.e. for both attacker and defender troops are modified depending on attacker hero, defending hero, attacker research and defender research.

**If the defender has wall, then the wall hitpoint is calculated by 100000*wall_capacity and for the attacker to win the battle, they have to kill the wall as well**

There are two type of troops, ranged troops and melee troops in battle.
### Ranged Troops:-

> Archer, Ballista, Catapult

### Melee Troops:-

> Cavalry, Cataphract, Worker, Warrior, Sword, Pike, Scout, Transporters, Ram

The whole battle consists of rounds and it continues till it reaches 100 rounds or one side is dead (for defender the wall hitpoint is considered too as mentioned before). A round consists of two parts:- **movement** and then after all movement actions are complete, **attack**.

The battlefield size is calculated using the following formula:-

> field_size=200+max(range_of_all_troops_on_both_sides_including_fortification)

The defender troops start at position 0 in the battle field and the attacker troops start at the end of the battle field, i.e. at position field_size.

### Movement:
There are some rules for troop movement:-
* The only way scouts and transporters on the attacker side can move is if there are no troops of other types left on the attacker side. The scouts and transporters from the defender side never move.
* All the troops from both side move using the following movement order:- 
> Scout, Cavalry, Cataphract, Pike, Sword, Warrior, Worker, Transporter, Ram, Archer, Ballista, Catapult
* If troops of same type are there from both defender and attacker side, then defender troop of that type moves first, then attacker.
* If a troop has another troop of the opposite side in range, it won't move.
* A troop can't cross a troop from the opposing side while moving, so it moves until it reaches a troop from another side with the maximum distance it can move in one round being its speed value.

### Attack:
For this part, first the troops from both side attack troops from the opposing side in the following order:-
> Scout, Cavalry, Cataphract, Pike, Sword, Archer, Warrior, Worker, Transporter, Ram, Ballista, Catapult

**Note: This attack order is slightly different from the movement order**

After each troop attacks another troop, the attacked troop's **effective troop count** is reduced by the amount of troops killed. This **effective troop count** is used for target selection, i.e. for example if the effective troop count of a troop is 0, no other troop from the opposite side will attack it during the round.

Of course, when a troop attacks another troop, the **effective troop count** of the attacker doesn't matter, but instead, the **original troop count at the beginning of the current round** is used to calculate how much damage it deals.

At the end of the round, the troop count for each troop type is set to its current **effective troop count**.

After the troops have attacked each other from both side, fortification battle begins, i.e. different fortifications start attacking the attacker troops.

So, for a troop to attack another troop, there are two parts in this process:-

#### Target Choice:
Here are the rules for target choice:-
* If the attacking troop is a ranged troop:-
	 * If there is another ranged troop from the opposing side in its range, it will target the ranged troop in its range with maximum total attack value(total attack value=effective_troop_count*troop_attack). In this selection, for attacker troops, the defender's archer towers are considered as well.
	 * Otherwise, it will attack the melee troop with highest speed
	 * Otherwise, if the wall is in range and wall hitpoint > 0, then it will attack the wall.
 * Otherwise, if the attacking troop is a melee troop, it will target the troop with highest attack value (this also includes defender's archer towers when its a attacker troop). If no troop is in range and the wall is in range and wallhitpoint > 0, it will attack the wall instead.

#### Damage Calculation:
This are the rules for damage calculation:-
* If the attacking troop is a ranged troop:-
	```
	Damage = ceil(attacker_attack * attacker_count * defender_defense_effect * damage_modifier / defender_life_value)
	```
	
	In the above formula ceil means rounding up the number.
	Defender defense effect is calculated with the formula
	```
	defense_effect = (1000-defense_value)/1000
	```
	
	The damage modifier in this case depends on the distance of the targeted troop from the attacking troop
	```
	if (distance <= targeted_troop_range and targeted_troop_is_melee) damage_modifier = 0.25
	if (distance > attacker_range/2) damage_modifier=0.5
	otherwise damage_modifier=1
	```
	
* If the attacking troop is a melee troop, the damage calculation is similar to before, i.e.
	```
	killed = floor(attacker_attack * attacker_count * defender_defense_effect * damage_modifier / defender_life_value)
	```

	But in this case, the damage modifier is taken from the following table instead(if the value isn't present here, means its 1):-
	
|  | WO | W | SC | P | SW | A | T | C | CATA | B | R | CP | AT
|--|--|--|--|--|--|--|--|--|--|--|--|--|--|
|WO|||||||||||||0.3
|W|||||||||||||0.3
|SC|||||||||||||0.3
|P||||||||1.8|1.8||||0.3
|SW||||1.1|||||||||0.3
|A|||||||||||||0.3
|T|||||||||||||0.3
|C||||||1.2|||||||0.3
|CATA||||||1.2|||||||0.3
|B|||||||||||||0.3
|R|||||||||||||0.3
|CP|||||||||||||0.3

* As mentioned before, at the end of the round, the troop count of each troop is set to its current effective troop count.
### Fortification Battle
In the fortification battle, first the traps damage all troops, then abatis, then rolling logs, trebuchet and then archer towers.

#### Trap Attack
Range of traps is 5000, so if any troop is within 5000 distance from the wall and not reached the wall yet, it will get damaged by the traps.  Traps attack all troops equally. At the beginning of the battle a fixed trap rate is set **which is a random number between 5 and 9**. So, if the trap rate is **x**, then in each round if there is a troop in range of traps, **x% of the original trap count at the beginning of the battle** will fire in each round and damage every troop in range equally. So, if there are **y** troops in range of the traps in current round, each of them will be killed following this formula:-
```
killed= traps_fired_in_current_round * 0.5 * trapKillPower / y
```
The trap kill power is a number 0.05 and 2, the default being 1.25. (Not sure what this value depends on yet, but its a fixed number set at the beginning of the battle).

#### Abatis Attack
Range of abatis is 5000, so it will attack if there is a cavalry or cataphract in range of the wall and not reached the wall yet. Unlike traps, abatis only attacks a single troop in each round, so it will either attack cav or phracts, with the priority being cavs. In each round, if there is a cav or phract in range, **10% of the original abatis count at the beginning of battle** will fire and of course, if there is less than 10% abatis left, all of them will fire. The abatis damage on cavs or phracts is calculated using the following formula:-

```
killed = floor(abatis_fired_current_round * 0.535)
```

#### Rolling Log Attack
Range of rolling logs is 1300, so it will attack if there is a ground troop (i.e. worker or warrior or scout or pike or sword or archer) within 1300 distance of the wall and not reached the wall yet. Rolling log also attacks only one type of troop in each round. It attacks the ground troop with highest total attack value. Similar to abatis, if there is a ground troop in range, **10% of the original rolling log count at the beginning of the battle** will fire and of course, if there is less than 10% rolling log left, all of them will fire. The rolling log damage on the troop is calculated using the following formula:-

```
killed = floor(logs_fired * logKillPower * rolling_log_attack_value * defense_effect_of_the_troop_being_attacked)
```
Similar to abatis, logKillPower is also a fixed number between 0.05 and 2 set at the beginning of the battle, the default value being 1.25.

#### Trebuchet Attack
Range of trebuchet is 5000, so it will attack if there is any troop within 5000 distance from the wall and not reached the wall yet. Unlike traps, abatis or rolling logs, the trebuchet fired in each round is not a fixed number. Trebuchet attacks the troop with highest total attack value. In each round, **maximum 10% of the original trebuchet count at the beginning of the battle will fire**. The damage from a single trebuchet can be calculated using the following formula(the number of killed troops from a single trebuchet can be a floating point number of course):-
```
killed = trebuchet_attack * tebuchetillPower / (defense_of_troop_being_attacked * life_of_troop_being_attacked)
```
(Similar to before, the trebuchetKillPower is also a fixed number between 0.05 and 2 set at the beginning of the battle, the default being 1.25)

So, in each round, as many trebuchets as needed will fire to kill as many troops as it can from the troop type it selects to kill. Of course, the maximum amount of trebuchet that can fire is 10% of the original number of trebuchets.

#### Archer Tower Attack

Archer tower attacking is similar to ranged troop attacking, i.e. the target selection is same, and for this particular fortification, it will still attack even if a attacking troop has reached the wall. The damage modifier in this case is slightly different:-
```
if (distance > archer_tower_range/2) damage_modifier=0.5
otherwise damage_modifier=1
```
The damage calculation is also different for archer towers:-
```
killed = floor(archerTower_attack * archerTower_count * damageModifier / (defense_of_troop_being_attacked * life_of_troop_being_attacked))
```

#### In all calculation, wherever floating point is used, it has precision 15 bit, i.e same as the "float" type in C++, not the type "double"
