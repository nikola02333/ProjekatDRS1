using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Biblioteka.Data;

namespace ProjTest.DataTest
{
    [TestFixture]
    public class OsobaTest
    {
        private Osoba osobaTest;
        private string korime = "Nikola";
        private string pass = "nikola023";
        private  string uloga ="direktor";
        private string radVremePoc = "10:00";
        private string radVremeKraj = "16:00";
        private bool prijavljen = false;
        private DateTime vremeLozinke = DateTime.Now;
        private string email = "svetabombas@live.com";
        [OneTimeSetUp]
        public void SetupTest()
        {
            this.osobaTest = new Osoba();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.DoesNotThrow(() => new Osoba());
        }

        [Test]
        [TestCase("Nikola", "nikola", "direktor", "9:00", "12:00", true, "svetabombas@live.com")]
        public void ContructorWithParametar(string korime, string pass, string uloga, string radVremePoc, string radVremeKraj, bool prijava, string email)
        {
            DateTime vremeLozinke = DateTime.Now;

            Osoba o = new Osoba(korime, pass, uloga, radVremePoc, radVremeKraj, prijava, vremeLozinke, email);

            Assert.AreEqual(o.KorIme, korime);
            Assert.AreEqual(o.Lozinka, pass);
            Assert.AreEqual(o.Uloga, uloga);
            Assert.AreEqual(o.RadnoVremeStart, radVremePoc);
            Assert.AreEqual(o.RadnoVremeKraj, radVremeKraj);
            Assert.AreEqual(o.Prijavljen, prijava);
            Assert.AreEqual(o.VremeLozinke, vremeLozinke);
            Assert.AreEqual(o.Email, email);
        }

        [Test]
        public void IdTest()
        {
            int id = 1;
            osobaTest.Id = id;

            Assert.AreEqual(id, osobaTest.Id);
        }
        [Test]
        public void KorImeTest()
        {
            osobaTest.KorIme = korime;

            Assert.AreEqual(korime, osobaTest.KorIme);
        }
        [Test]
        public void LozinkaTest()
        {
            osobaTest.Lozinka = pass;

            Assert.AreEqual(pass, osobaTest.Lozinka);
        }
        [Test]
        public void UlogaTest()
        {
            osobaTest.Uloga = uloga;

            Assert.AreEqual(uloga, osobaTest.Uloga);
        }
        [Test]
        public void RadnoVremeStartTest()
        {
            osobaTest.RadnoVremeStart = radVremePoc;

            Assert.AreEqual(radVremePoc, osobaTest.RadnoVremeStart);
        }
        [Test]
        public void RadnoVremeKrajTest()
        {
            osobaTest.RadnoVremeKraj = radVremeKraj;

            Assert.AreEqual(radVremeKraj, osobaTest.RadnoVremeKraj);
        }
        [Test]
        public void PrijavljenTest()
        {
            osobaTest.Prijavljen = prijavljen;

            Assert.AreEqual(prijavljen, osobaTest.Prijavljen);
        }
        [Test]
        public void VremeLozinkeTest()
        {
            osobaTest.VremeLozinke = vremeLozinke;

            Assert.AreEqual(vremeLozinke, osobaTest.VremeLozinke);
        }
        [Test]
        public void EmailTest()
        {
            osobaTest.Email = email;

            Assert.AreEqual(email, osobaTest.Email);
        }


    }
}
