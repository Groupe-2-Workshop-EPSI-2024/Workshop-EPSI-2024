package com.example.myapplicationnfc

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.ListView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class AddictionListActivity : AppCompatActivity() {

    private lateinit var listView: ListView
    private lateinit var contactDoctorButton: Button
    private val addictions = listOf("Tabagisme", "Alcoolisme", "Drogues", "Jeux d'argent") // Example list

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_addiction_list)

        listView = findViewById(R.id.addictionListView)
        contactDoctorButton = findViewById(R.id.contactDoctorButton)

        val adapter = ArrayAdapter(this, android.R.layout.simple_list_item_1, addictions)
        listView.adapter = adapter

        listView.setOnItemClickListener { _, _, position, _ ->
            val selectedAddiction = addictions[position]
            val intent = Intent(this, SobrietyTrackerActivity::class.java)
            intent.putExtra("addictionType", selectedAddiction) // Pass addiction type to the new activity
            startActivity(intent)
        }

        contactDoctorButton.setOnClickListener {
            // For testing, log the intent to simulate sending an email if no email app is found
            sendEmailToDoctor()
        }
    }

    private fun sendEmailToDoctor() {
//        val emailIntent = Intent(Intent.ACTION_SENDTO).apply {
//            data = Uri.parse("mailto:") // Only email apps should handle this
//            putExtra(Intent.EXTRA_EMAIL, arrayOf("docteur@example.com")) // Replace with doctor's email
//            putExtra(Intent.EXTRA_SUBJECT, "Besoin d'aide pour mon suivi de sobriété")
//            putExtra(Intent.EXTRA_TEXT, "Bonjour docteur,\n\nJe voudrais discuter de mon suivi de sobriété. Merci.")
//        }
//
//        if (emailIntent.resolveActivity(packageManager) != null) {
//            startActivity(emailIntent)
//        } else {
//            // Log the action instead of trying to send an email on the emulator
//            Log.i("Email", "No email app available on the emulator. Simulating sending email.")
//            Toast.makeText(this, "Aucune application e-mail trouvée, enregistrement dans les logs.", Toast.LENGTH_SHORT).show()
//        }
        val email = "xxxx@xxxx.com"
        val subject = "xxxx"
        val body = "xxxx"

        val selectorIntent = Intent(Intent.ACTION_SENDTO)
        val urlString = "mailto:" + Uri.encode(email) + "?subject=" + Uri.encode(subject) + "&body=" + Uri.encode(body)
        selectorIntent.data = Uri.parse(urlString)

        val emailIntent = Intent(Intent.ACTION_SEND)
        emailIntent.putExtra(Intent.EXTRA_EMAIL, arrayOf(email))
        emailIntent.putExtra(Intent.EXTRA_SUBJECT, subject)
        emailIntent.putExtra(Intent.EXTRA_TEXT, body)
        emailIntent.selector = selectorIntent

        startActivity(Intent.createChooser(emailIntent, "Send email"))
    }
}
