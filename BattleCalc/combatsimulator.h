#include <stdint.h>
#include <array>

#define DLLExport __declspec( dllexport )  

struct troopStat {
	double life;
	double attack;
	double defense;
	double speed;
	double range;
};
struct researchStats {
	int32_t military_tradition=0;
	int32_t iron_working=0;
	int32_t medicine=0;
	int32_t compass=0;
	int32_t horseback_riding=0;
	int32_t archery=0;
	int32_t machinery=0;
};
struct heroStat{
	double attack=0;
	double intel=0;
	double leadership=0;
	int8_t ok = false;
};
struct attacker {
	researchStats research;
	heroStat hero;
	int64_t troops[12] = { 0,0,0,0,0,0,0,0,0,0,0,0 };
	double attack_modifier = 1;
	double defence_modifier = 1;
	double life_modifier = 1;
};
struct defender {
	researchStats research;
	heroStat hero;
	double attack_modifier = 1;
	double defence_modifier = 1;
	double life_modifier = 1;
	int64_t troops[12] = { 0,0,0,0,0,0,0,0,0,0,0,0 };
	int64_t fortifications[5] = { 0,0,0,0,0 };
	int32_t wallLevel=0;
	int8_t gateOpen;
};
struct combatTroops {
	int32_t location;
	int32_t typeId;
	int64_t count = 0;
	troopStat* stat;
	double damage = 0;
	int8_t isAttacker = false;
};
struct battleResult {
	int64_t attackerTroops[12] = { 0,0,0,0,0,0,0,0,0,0,0,0 };
	int64_t defenderTroops[12] = { 0,0,0,0,0,0,0,0,0,0,0,0 };
	int64_t fortification[5] = { 0,0,0,0,0 };
	int32_t result;
	int32_t totalRounds;
};
class DLLExport CombatSimulator {
	public:
		static troopStat baseStats[12];
		static troopStat baseFortificationStats[5];
		static int8_t troopTypes[12];
		static int32_t movementOrder[12];
		static int32_t nonRangedTroops[9];
		static void modifyStats(troopStat* base, researchStats res, heroStat hero, double atk_modifier, double def_modifier, double life_modifier);
		static std::array<int32_t,5> meleeTroopTypes;
		static std::array<int32_t,3> rangedTroopTypes;
		static std::array<int32_t,3> mechTroopTypes;
		static std::array<int32_t, 6> groundTroopTypes;
		static std::array<int32_t, 2> mountedTroopTypes;
		static double damageModifiers[12][12];
		static void fight(attacker atk, defender def, battleResult* res);
	private:
		static int8_t compareSpeed(combatTroops& x, combatTroops& y);
};

extern "C" __declspec(dllexport) void simulateBattle(attacker atk, defender def, battleResult* res) {
	CombatSimulator::fight(atk, def, res);
};