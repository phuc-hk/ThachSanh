using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestingDamage
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestingDamageSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestingDamageWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    
    [TestCase(1, 2, ExpectedResult = 2)]

    public int When_Multiple_2_Number_Expect_Multiple_Result(int a, int b)
    {
        Calculator calculator = new Calculator();
        return calculator.Multiple(a, b);
    }
}
