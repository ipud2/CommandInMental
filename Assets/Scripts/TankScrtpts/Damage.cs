using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSTank
{
    public class Damage
    {
        public int damageSenderId;
        public int damageSenderTeamNumber;
        public float attackPoint;

        public Damage()
        {
            damageSenderId = 0;
            damageSenderTeamNumber = 0;
            attackPoint = 0;
        }
    }
}
