
namespace control_de_stocks
{
    partial class FormDeudores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDeudores = new System.Windows.Forms.DataGridView();
            this.btnDeudor = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeudores)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDeudores
            // 
            this.dgvDeudores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeudores.Location = new System.Drawing.Point(12, 26);
            this.dgvDeudores.Name = "dgvDeudores";
            this.dgvDeudores.RowHeadersWidth = 51;
            this.dgvDeudores.RowTemplate.Height = 24;
            this.dgvDeudores.Size = new System.Drawing.Size(789, 439);
            this.dgvDeudores.TabIndex = 0;
            // 
            // btnDeudor
            // 
            this.btnDeudor.Location = new System.Drawing.Point(836, 46);
            this.btnDeudor.Name = "btnDeudor";
            this.btnDeudor.Size = new System.Drawing.Size(113, 92);
            this.btnDeudor.TabIndex = 1;
            this.btnDeudor.Text = "Nuevo Deudor";
            this.btnDeudor.UseVisualStyleBackColor = true;
            this.btnDeudor.Click += new System.EventHandler(this.btnDeudor_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(836, 182);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(113, 57);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.Text = "Actualizar Deuda";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(836, 289);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(113, 68);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar Deuda";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormDeudores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 519);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnDeudor);
            this.Controls.Add(this.dgvDeudores);
            this.Name = "FormDeudores";
            this.Text = "FormDeudores";
            this.Load += new System.EventHandler(this.FormDeudores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeudores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeudores;
        private System.Windows.Forms.Button btnDeudor;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCancelar;
    }
}