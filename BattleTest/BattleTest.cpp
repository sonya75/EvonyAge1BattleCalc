// BattleTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../BattleCalc/combatsimulator.h"
#include <iostream>

int main()
{
	attacker x;
	defender y;
	x.troops[11] = 1000;
	y.troops[7] = 9999999;
	battleResult z;
	CombatSimulator::fight(x, y, &z);
	std::getchar();
}

