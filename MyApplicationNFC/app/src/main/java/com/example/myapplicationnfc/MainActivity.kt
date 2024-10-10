package com.example.myapplicationnfc

import android.app.PendingIntent
import android.content.Intent
import android.content.SharedPreferences
import android.nfc.NfcAdapter
import android.nfc.Tag
import android.nfc.tech.Ndef
import android.nfc.NdefMessage
import android.nfc.NdefRecord
import android.os.Bundle
import android.provider.Settings
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {

    private lateinit var nfcAdapter: NfcAdapter
    private lateinit var textView: TextView
    private lateinit var passwordEditText: EditText
    private lateinit var connectButton: Button
    private lateinit var writeButton: Button

    private val PREFS_NAME = "MyPrefsFile"
    private val PASSWORD_KEY = "password"
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        nfcAdapter = NfcAdapter.getDefaultAdapter(this)
        textView = findViewById(R.id.textView)
        passwordEditText = findViewById(R.id.passwordEditText)
        connectButton = findViewById(R.id.connectButton)
        writeButton = findViewById(R.id.writeButton)

        sharedPreferences = getSharedPreferences(PREFS_NAME, MODE_PRIVATE)

        // Initialisation du mot de passe lors de la première connexion
        connectButton.setOnClickListener {
            handleConnection()
        }

        // Gestion de l'écriture de données sur la carte NFC
        writeButton.setOnClickListener {
            if (nfcAdapter == null) {
                Toast.makeText(this, "NFC is not supported on this device.", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            if (!nfcAdapter.isEnabled) {
                Toast.makeText(this, "Please enable NFC.", Toast.LENGTH_SHORT).show()
                startActivity(Intent(Settings.ACTION_NFC_SETTINGS))
                return@setOnClickListener
            }

            // Activation du mode de réception NFC
            val pendingIntent = PendingIntent.getActivity(
                this, 0, Intent(this, javaClass).addFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP), PendingIntent.FLAG_UPDATE_CURRENT
            )
            nfcAdapter.enableForegroundDispatch(this, pendingIntent, null, null)
        }
    }

    private fun handleConnection() {
        val enteredPassword = passwordEditText.text.toString()
        val storedPassword = sharedPreferences.getString(PASSWORD_KEY, "123456") // Mot de passe par défaut

        if (enteredPassword == storedPassword) {
            Toast.makeText(this, "Mot de passe correct. Redirection vers l'accueil.", Toast.LENGTH_SHORT).show()
            // Démarrer HomeActivity après une connexion réussie
            val intent = Intent(this, HomeActivity::class.java)
            startActivity(intent)
            finish() // Fermer MainActivity
        } else {
            Toast.makeText(this, "Mot de passe incorrect. Réessayez.", Toast.LENGTH_SHORT).show()
        }
    }

    override fun onNewIntent(intent: Intent) {
        super.onNewIntent(intent)

        if (NfcAdapter.ACTION_NDEF_DISCOVERED == intent?.action) {
            val tag = intent.getParcelableExtra<Tag>(NfcAdapter.EXTRA_TAG)
            tag?.let {
                writeNfcTag("Health data stored successfully!", it)
            }
        }
    }

    private fun writeNfcTag(data: String, tag: Tag) {
        val ndefRecord = NdefRecord.createTextRecord("en", data)
        val ndefMessage = NdefMessage(arrayOf(ndefRecord))

        try {
            val ndef = Ndef.get(tag)
            ndef?.let {
                it.connect()
                it.writeNdefMessage(ndefMessage)
                it.close()
                Toast.makeText(this, "Données écrites sur la carte NFC.", Toast.LENGTH_SHORT).show()
            } ?: run {
                Toast.makeText(this, "La carte NFC n'est pas formatée en NDEF.", Toast.LENGTH_SHORT).show()
            }
        } catch (e: Exception) {
            Log.e("NFC", "Erreur lors de l'écriture sur la carte NFC", e)
        }
    }
}
