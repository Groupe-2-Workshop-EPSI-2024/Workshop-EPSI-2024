package com.example.myapplicationnfc

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.ListView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class AddictionListActivity : AppCompatActivity() {

    private lateinit var listView: ListView
    private lateinit var contactDoctorButton: Button
    private val addictions = listOf("Tabagisme", "Alcoolisme", "Drogues", "Jeux d'argent") // Liste d'exemple

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_addiction_list)

        listView = findViewById(R.id.addictionListView)
        contactDoctorButton = findViewById(R.id.contactDoctorButton)

        val adapter = ArrayAdapter(this, android.R.layout.simple_list_item_1, addictions)
        listView.adapter = adapter

        listView.setOnItemClickListener { _, _, position, _ ->
            // Quand un élément de la liste est cliqué, lancer une activité de suivi de sobriété
            val selectedAddiction = addictions[position]
            val intent = Intent(this, SobrietyTrackerActivity::class.java)
            intent.putExtra("addictionType", selectedAddiction) // Passer le type d'addiction à la nouvelle activité
            startActivity(intent)
        }

        contactDoctorButton.setOnClickListener {
            sendEmailToDoctor()
        }
    }

    private fun sendEmailToDoctor() {
        // Créer une intention pour ouvrir Gmail avec un e-mail pré-rempli
        val emailIntent = Intent(Intent.ACTION_SENDTO).apply {
            data = Uri.parse("mailto:") // Seules les applications e-mail devraient gérer cette intention
            putExtra(Intent.EXTRA_EMAIL, arrayOf("docteur@example.com")) // Remplace par l'e-mail de ton médecin
            putExtra(Intent.EXTRA_SUBJECT, "Besoin d'aide pour mon suivi de sobriété")
            putExtra(Intent.EXTRA_TEXT, "Bonjour docteur,\n\nJe voudrais discuter de mon suivi de sobriété. Merci.")
        }

        // Vérifier si une application e-mail est disponible
        if (emailIntent.resolveActivity(packageManager) != null) {
            startActivity(emailIntent)
        } else {
            Toast.makeText(this, "Aucune application e-mail trouvée.", Toast.LENGTH_SHORT).show()
        }
    }
}
