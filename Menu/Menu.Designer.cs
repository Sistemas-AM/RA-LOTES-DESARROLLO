namespace Menu
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotizacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotizacionesTechumbreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeCobranzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeComprasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasACreditoPorSucursalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasDiariasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasPorUnidadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeDevolucionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeDescuentosPorProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasPorSucursalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasCajaGeneralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasPlantaCajaGeneralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteInventarioFisicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeVentasContabilidadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteCapacidadVentaYSurtidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatoDeSolidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteMaterialNoConformeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeCapasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.corteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.corteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.corteGeneralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatoCorteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargaArchivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalagoDePreciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distribucionDeMaterialesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surtidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distribucionMaterialesSucursalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipoDeCambioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargaMaximosMinimosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeTraspasosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportesToolStripMenuItem,
            this.reportesToolStripMenuItem1,
            this.corteToolStripMenuItem,
            this.otrosToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip1.Size = new System.Drawing.Size(111, 394);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cotizacionToolStripMenuItem,
            this.cotizacionesToolStripMenuItem,
            this.cotizacionesTechumbreToolStripMenuItem});
            this.reportesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(98, 23);
            this.reportesToolStripMenuItem.Text = "Cotizaciones";
            // 
            // cotizacionToolStripMenuItem
            // 
            this.cotizacionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cotizacionToolStripMenuItem.Name = "cotizacionToolStripMenuItem";
            this.cotizacionToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.cotizacionToolStripMenuItem.Text = "Ventas Especiales";
            this.cotizacionToolStripMenuItem.Click += new System.EventHandler(this.cotizacionToolStripMenuItem_Click);
            // 
            // cotizacionesToolStripMenuItem
            // 
            this.cotizacionesToolStripMenuItem.Name = "cotizacionesToolStripMenuItem";
            this.cotizacionesToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.cotizacionesToolStripMenuItem.Text = "Cotizaciones";
            this.cotizacionesToolStripMenuItem.Click += new System.EventHandler(this.cotizacionesToolStripMenuItem_Click);
            // 
            // cotizacionesTechumbreToolStripMenuItem
            // 
            this.cotizacionesTechumbreToolStripMenuItem.Name = "cotizacionesTechumbreToolStripMenuItem";
            this.cotizacionesTechumbreToolStripMenuItem.Size = new System.Drawing.Size(240, 24);
            this.cotizacionesTechumbreToolStripMenuItem.Text = "Cotizaciones Techumbre";
            this.cotizacionesTechumbreToolStripMenuItem.Click += new System.EventHandler(this.cotizacionesTechumbreToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem1
            // 
            this.reportesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteDeCobranzaToolStripMenuItem,
            this.reporteDeComprasToolStripMenuItem,
            this.reporteDeVentasACreditoPorSucursalToolStripMenuItem,
            this.reporteDeVentasDiariasToolStripMenuItem,
            this.reporteDeVentasPorUnidadesToolStripMenuItem,
            this.reporteDeDevolucionesToolStripMenuItem,
            this.reporteDeDescuentosPorProductosToolStripMenuItem,
            this.reporteDeVentasPorSucursalToolStripMenuItem,
            this.reporteDeVentasCajaGeneralToolStripMenuItem,
            this.reporteDeVentasPlantaCajaGeneralToolStripMenuItem,
            this.reporteInventarioFisicoToolStripMenuItem,
            this.reporteDeVentasContabilidadToolStripMenuItem,
            this.reporteCapacidadVentaYSurtidoToolStripMenuItem,
            this.formatoDeSolidosToolStripMenuItem,
            this.reporteMaterialNoConformeToolStripMenuItem,
            this.reporteDeCapasToolStripMenuItem});
            this.reportesToolStripMenuItem1.Name = "reportesToolStripMenuItem1";
            this.reportesToolStripMenuItem1.Size = new System.Drawing.Size(98, 23);
            this.reportesToolStripMenuItem1.Text = "Reportes";
            // 
            // reporteDeCobranzaToolStripMenuItem
            // 
            this.reporteDeCobranzaToolStripMenuItem.Name = "reporteDeCobranzaToolStripMenuItem";
            this.reporteDeCobranzaToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeCobranzaToolStripMenuItem.Text = "Reporte de cobranza";
            this.reporteDeCobranzaToolStripMenuItem.Click += new System.EventHandler(this.reporteDeCobranzaToolStripMenuItem_Click);
            // 
            // reporteDeComprasToolStripMenuItem
            // 
            this.reporteDeComprasToolStripMenuItem.Name = "reporteDeComprasToolStripMenuItem";
            this.reporteDeComprasToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeComprasToolStripMenuItem.Text = "Reporte de compras";
            this.reporteDeComprasToolStripMenuItem.Click += new System.EventHandler(this.reporteDeComprasToolStripMenuItem_Click);
            // 
            // reporteDeVentasACreditoPorSucursalToolStripMenuItem
            // 
            this.reporteDeVentasACreditoPorSucursalToolStripMenuItem.Name = "reporteDeVentasACreditoPorSucursalToolStripMenuItem";
            this.reporteDeVentasACreditoPorSucursalToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeVentasACreditoPorSucursalToolStripMenuItem.Text = "Reporte de ventas a credito por sucursal";
            this.reporteDeVentasACreditoPorSucursalToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasACreditoPorSucursalToolStripMenuItem_Click);
            // 
            // reporteDeVentasDiariasToolStripMenuItem
            // 
            this.reporteDeVentasDiariasToolStripMenuItem.Name = "reporteDeVentasDiariasToolStripMenuItem";
            this.reporteDeVentasDiariasToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeVentasDiariasToolStripMenuItem.Text = "Reporte de Ventas Diarias";
            this.reporteDeVentasDiariasToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasDiariasToolStripMenuItem_Click);
            // 
            // reporteDeVentasPorUnidadesToolStripMenuItem
            // 
            this.reporteDeVentasPorUnidadesToolStripMenuItem.Name = "reporteDeVentasPorUnidadesToolStripMenuItem";
            this.reporteDeVentasPorUnidadesToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeVentasPorUnidadesToolStripMenuItem.Text = "Reporte de Ventas por Unidades";
            this.reporteDeVentasPorUnidadesToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasPorUnidadesToolStripMenuItem_Click);
            // 
            // reporteDeDevolucionesToolStripMenuItem
            // 
            this.reporteDeDevolucionesToolStripMenuItem.Name = "reporteDeDevolucionesToolStripMenuItem";
            this.reporteDeDevolucionesToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeDevolucionesToolStripMenuItem.Text = "Reporte de Devoluciones";
            this.reporteDeDevolucionesToolStripMenuItem.Click += new System.EventHandler(this.reporteDeDevolucionesToolStripMenuItem_Click);
            // 
            // reporteDeDescuentosPorProductosToolStripMenuItem
            // 
            this.reporteDeDescuentosPorProductosToolStripMenuItem.Name = "reporteDeDescuentosPorProductosToolStripMenuItem";
            this.reporteDeDescuentosPorProductosToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeDescuentosPorProductosToolStripMenuItem.Text = "Reporte de Descuentos por productos";
            this.reporteDeDescuentosPorProductosToolStripMenuItem.Click += new System.EventHandler(this.reporteDeDescuentosPorProductosToolStripMenuItem_Click);
            // 
            // reporteDeVentasPorSucursalToolStripMenuItem
            // 
            this.reporteDeVentasPorSucursalToolStripMenuItem.Name = "reporteDeVentasPorSucursalToolStripMenuItem";
            this.reporteDeVentasPorSucursalToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeVentasPorSucursalToolStripMenuItem.Text = "Reporte de Ventas por Sucursal";
            this.reporteDeVentasPorSucursalToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasPorSucursalToolStripMenuItem_Click);
            // 
            // reporteDeVentasCajaGeneralToolStripMenuItem
            // 
            this.reporteDeVentasCajaGeneralToolStripMenuItem.Name = "reporteDeVentasCajaGeneralToolStripMenuItem";
            this.reporteDeVentasCajaGeneralToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeVentasCajaGeneralToolStripMenuItem.Text = "Reporte de Ventas caja general";
            this.reporteDeVentasCajaGeneralToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasCajaGeneralToolStripMenuItem_Click);
            // 
            // reporteDeVentasPlantaCajaGeneralToolStripMenuItem
            // 
            this.reporteDeVentasPlantaCajaGeneralToolStripMenuItem.Name = "reporteDeVentasPlantaCajaGeneralToolStripMenuItem";
            this.reporteDeVentasPlantaCajaGeneralToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeVentasPlantaCajaGeneralToolStripMenuItem.Text = "Reporte de Ventas Planta caja general";
            this.reporteDeVentasPlantaCajaGeneralToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasPlantaCajaGeneralToolStripMenuItem_Click);
            // 
            // reporteInventarioFisicoToolStripMenuItem
            // 
            this.reporteInventarioFisicoToolStripMenuItem.Name = "reporteInventarioFisicoToolStripMenuItem";
            this.reporteInventarioFisicoToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteInventarioFisicoToolStripMenuItem.Text = "Reporte Inventario Fisico";
            this.reporteInventarioFisicoToolStripMenuItem.Click += new System.EventHandler(this.reporteInventarioFisicoToolStripMenuItem_Click);
            // 
            // reporteDeVentasContabilidadToolStripMenuItem
            // 
            this.reporteDeVentasContabilidadToolStripMenuItem.Name = "reporteDeVentasContabilidadToolStripMenuItem";
            this.reporteDeVentasContabilidadToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeVentasContabilidadToolStripMenuItem.Text = "Reporte de ventas contabilidad";
            this.reporteDeVentasContabilidadToolStripMenuItem.Click += new System.EventHandler(this.reporteDeVentasContabilidadToolStripMenuItem_Click);
            // 
            // reporteCapacidadVentaYSurtidoToolStripMenuItem
            // 
            this.reporteCapacidadVentaYSurtidoToolStripMenuItem.Name = "reporteCapacidadVentaYSurtidoToolStripMenuItem";
            this.reporteCapacidadVentaYSurtidoToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteCapacidadVentaYSurtidoToolStripMenuItem.Text = "Reporte Capacidad Venta y Surtido";
            this.reporteCapacidadVentaYSurtidoToolStripMenuItem.Click += new System.EventHandler(this.reporteCapacidadVentaYSurtidoToolStripMenuItem_Click);
            // 
            // formatoDeSolidosToolStripMenuItem
            // 
            this.formatoDeSolidosToolStripMenuItem.Name = "formatoDeSolidosToolStripMenuItem";
            this.formatoDeSolidosToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.formatoDeSolidosToolStripMenuItem.Text = "Formato de Solidos";
            this.formatoDeSolidosToolStripMenuItem.Click += new System.EventHandler(this.formatoDeSolidosToolStripMenuItem_Click);
            // 
            // reporteMaterialNoConformeToolStripMenuItem
            // 
            this.reporteMaterialNoConformeToolStripMenuItem.Name = "reporteMaterialNoConformeToolStripMenuItem";
            this.reporteMaterialNoConformeToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteMaterialNoConformeToolStripMenuItem.Text = "Reporte Material No Conforme";
            this.reporteMaterialNoConformeToolStripMenuItem.Click += new System.EventHandler(this.reporteMaterialNoConformeToolStripMenuItem_Click);
            // 
            // reporteDeCapasToolStripMenuItem
            // 
            this.reporteDeCapasToolStripMenuItem.Name = "reporteDeCapasToolStripMenuItem";
            this.reporteDeCapasToolStripMenuItem.Size = new System.Drawing.Size(349, 24);
            this.reporteDeCapasToolStripMenuItem.Text = "Reporte de Capas";
            this.reporteDeCapasToolStripMenuItem.Click += new System.EventHandler(this.reporteDeCapasToolStripMenuItem_Click);
            // 
            // corteToolStripMenuItem
            // 
            this.corteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.corteToolStripMenuItem1,
            this.corteGeneralToolStripMenuItem,
            this.formatoCorteToolStripMenuItem});
            this.corteToolStripMenuItem.Name = "corteToolStripMenuItem";
            this.corteToolStripMenuItem.Size = new System.Drawing.Size(98, 23);
            this.corteToolStripMenuItem.Text = "Corte";
            // 
            // corteToolStripMenuItem1
            // 
            this.corteToolStripMenuItem1.Name = "corteToolStripMenuItem1";
            this.corteToolStripMenuItem1.Size = new System.Drawing.Size(176, 24);
            this.corteToolStripMenuItem1.Text = "Corte";
            this.corteToolStripMenuItem1.Click += new System.EventHandler(this.corteToolStripMenuItem1_Click);
            // 
            // corteGeneralToolStripMenuItem
            // 
            this.corteGeneralToolStripMenuItem.Name = "corteGeneralToolStripMenuItem";
            this.corteGeneralToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.corteGeneralToolStripMenuItem.Text = "Corte general";
            this.corteGeneralToolStripMenuItem.Click += new System.EventHandler(this.corteGeneralToolStripMenuItem_Click);
            // 
            // formatoCorteToolStripMenuItem
            // 
            this.formatoCorteToolStripMenuItem.Name = "formatoCorteToolStripMenuItem";
            this.formatoCorteToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.formatoCorteToolStripMenuItem.Text = "Formato Corte";
            this.formatoCorteToolStripMenuItem.Click += new System.EventHandler(this.formatoCorteToolStripMenuItem_Click);
            // 
            // otrosToolStripMenuItem
            // 
            this.otrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargaArchivosToolStripMenuItem,
            this.catalagoDePreciosToolStripMenuItem,
            this.distribucionDeMaterialesToolStripMenuItem,
            this.pedidoToolStripMenuItem,
            this.surtidoToolStripMenuItem,
            this.distribucionMaterialesSucursalesToolStripMenuItem,
            this.tipoDeCambioToolStripMenuItem,
            this.cargaMaximosMinimosToolStripMenuItem,
            this.reporteDeTraspasosToolStripMenuItem});
            this.otrosToolStripMenuItem.Name = "otrosToolStripMenuItem";
            this.otrosToolStripMenuItem.Size = new System.Drawing.Size(98, 23);
            this.otrosToolStripMenuItem.Text = "Otros";
            // 
            // cargaArchivosToolStripMenuItem
            // 
            this.cargaArchivosToolStripMenuItem.Name = "cargaArchivosToolStripMenuItem";
            this.cargaArchivosToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.cargaArchivosToolStripMenuItem.Text = "Carga archivos";
            this.cargaArchivosToolStripMenuItem.Click += new System.EventHandler(this.cargaArchivosToolStripMenuItem_Click);
            // 
            // catalagoDePreciosToolStripMenuItem
            // 
            this.catalagoDePreciosToolStripMenuItem.Name = "catalagoDePreciosToolStripMenuItem";
            this.catalagoDePreciosToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.catalagoDePreciosToolStripMenuItem.Text = "Catalago de precios";
            this.catalagoDePreciosToolStripMenuItem.Click += new System.EventHandler(this.catalagoDePreciosToolStripMenuItem_Click);
            // 
            // distribucionDeMaterialesToolStripMenuItem
            // 
            this.distribucionDeMaterialesToolStripMenuItem.Name = "distribucionDeMaterialesToolStripMenuItem";
            this.distribucionDeMaterialesToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.distribucionDeMaterialesToolStripMenuItem.Text = "Distribucion de materiales";
            this.distribucionDeMaterialesToolStripMenuItem.Click += new System.EventHandler(this.distribucionDeMaterialesToolStripMenuItem_Click);
            // 
            // pedidoToolStripMenuItem
            // 
            this.pedidoToolStripMenuItem.Name = "pedidoToolStripMenuItem";
            this.pedidoToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.pedidoToolStripMenuItem.Text = "Pedido";
            this.pedidoToolStripMenuItem.Click += new System.EventHandler(this.pedidoToolStripMenuItem_Click);
            // 
            // surtidoToolStripMenuItem
            // 
            this.surtidoToolStripMenuItem.Name = "surtidoToolStripMenuItem";
            this.surtidoToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.surtidoToolStripMenuItem.Text = "Surtido para sucursales Accesorios";
            this.surtidoToolStripMenuItem.Click += new System.EventHandler(this.surtidoToolStripMenuItem_Click);
            // 
            // distribucionMaterialesSucursalesToolStripMenuItem
            // 
            this.distribucionMaterialesSucursalesToolStripMenuItem.Name = "distribucionMaterialesSucursalesToolStripMenuItem";
            this.distribucionMaterialesSucursalesToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.distribucionMaterialesSucursalesToolStripMenuItem.Text = "Distribucion materiales sucursales";
            this.distribucionMaterialesSucursalesToolStripMenuItem.Click += new System.EventHandler(this.distribucionMaterialesSucursalesToolStripMenuItem_Click);
            // 
            // tipoDeCambioToolStripMenuItem
            // 
            this.tipoDeCambioToolStripMenuItem.Name = "tipoDeCambioToolStripMenuItem";
            this.tipoDeCambioToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.tipoDeCambioToolStripMenuItem.Text = "Tipo de Cambio";
            this.tipoDeCambioToolStripMenuItem.Click += new System.EventHandler(this.tipoDeCambioToolStripMenuItem_Click);
            // 
            // cargaMaximosMinimosToolStripMenuItem
            // 
            this.cargaMaximosMinimosToolStripMenuItem.Name = "cargaMaximosMinimosToolStripMenuItem";
            this.cargaMaximosMinimosToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.cargaMaximosMinimosToolStripMenuItem.Text = "Carga Maximos Minimos";
            this.cargaMaximosMinimosToolStripMenuItem.Click += new System.EventHandler(this.cargaMaximosMinimosToolStripMenuItem_Click);
            // 
            // reporteDeTraspasosToolStripMenuItem
            // 
            this.reporteDeTraspasosToolStripMenuItem.Name = "reporteDeTraspasosToolStripMenuItem";
            this.reporteDeTraspasosToolStripMenuItem.Size = new System.Drawing.Size(309, 24);
            this.reporteDeTraspasosToolStripMenuItem.Text = "Reporte de Traspasos";
            this.reporteDeTraspasosToolStripMenuItem.Click += new System.EventHandler(this.reporteDeTraspasosToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(98, 23);
            this.toolStripMenuItem1.Text = "Salir";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Menu.Properties.Resources.Blue_Background_Wallpaper_HD_16273;
            this.ClientSize = new System.Drawing.Size(646, 394);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes Amsa - Menú";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cotizacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cotizacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reporteDeCobranzaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeComprasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasACreditoPorSucursalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem corteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem corteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem corteGeneralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargaArchivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catalagoDePreciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distribucionDeMaterialesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasDiariasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatoCorteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasPorUnidadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeDevolucionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeDescuentosPorProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surtidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasPorSucursalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distribucionMaterialesSucursalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasCajaGeneralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasPlantaCajaGeneralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipoDeCambioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteInventarioFisicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargaMaximosMinimosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeVentasContabilidadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cotizacionesTechumbreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteCapacidadVentaYSurtidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otrosToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reporteDeTraspasosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatoDeSolidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteMaterialNoConformeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeCapasToolStripMenuItem;
    }
}

