package com.example.myapplicationnfc



import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import android.widget.Toast // Assurez-vous que cet import est présent
import androidx.appcompat.app.AppCompatActivity


class SobrietyTrackerActivity : AppCompatActivity() {

    private lateinit var sobrietyTextView: TextView
    private lateinit var increaseSobrietyButton: Button
    private var sobrietyCount = 0 // Compteur de sobriété

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sobriety_tracker)

        // Récupérer le type d'addiction passé depuis l'activité précédente
        val addictionType = intent.getStringExtra("addictionType") ?: "Unknown"

        sobrietyTextView = findViewById(R.id.sobrietyTextView)
        increaseSobrietyButton = findViewById(R.id.increaseSobrietyButton)

        // Afficher le type d'addiction
        sobrietyTextView.text = "Suivi de sobriété pour : $addictionType"

        // Logique pour augmenter le compteur de sobriété
        increaseSobrietyButton.setOnClickListener {
            sobrietyCount++
            Toast.makeText(this, "Sobriété augmentée ! Compteur : $sobrietyCount", Toast.LENGTH_SHORT).show()
            // Mettre à jour l'affichage
            updateSobrietyDisplay()
        }
    }

    private fun updateSobrietyDisplay() {
        sobrietyTextView.text = "Suivi de sobriété : $sobrietyCount jours"
    }
}
