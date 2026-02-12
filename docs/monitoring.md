---

# 📁 PostgreSQL Monitoring Stack (Docker)

This repository contains a containerized monitoring solution for PostgreSQL using the **Prometheus** and **Grafana** ecosystem. It is designed to track database health, resource consumption, and identify **slow queries** using the `pg_stat_statements` extension.

## 🚀 Quick Start

1. **Clone the repository:**
```bash
git clone https://github.com/r-leafar/SbRf.SmartSales.API.git
cd SbRf.SmartSales.API

```


2. **Start the stack:**
```bash
docker-compose up -d

```


3. **Access the interfaces:**
* **Grafana:** [http://localhost:3000](https://www.google.com/search?q=http://localhost:3000) (Default: `admin` / `admin`)
* **Prometheus:** [http://localhost:9090](https://www.google.com/search?q=http://localhost:9090)
* **Postgres Exporter:** [http://localhost:9187/metrics](https://www.google.com/search?q=http://localhost:9187/metrics)



---

## 🏗️ Components

| Component | Role | Port |
| --- | --- | --- |
| **PostgreSQL** | Primary Database (with `pg_stat_statements`) | 5432 |
| **Postgres Exporter** | Scrapes SQL metrics and exposes them to Prometheus | 9187 |
| **Prometheus** | Time-series database for metric storage | 9090 |
| **Grafana** | Visualization and Alerting dashboard | 3000 |

---

## 📊 Dashboard Setup (ID: 9628)

We use the community-standard **PostgreSQL Database** dashboard.

1. Login to **Grafana**.
2. Navigate to **Dashboards** > **New** > **Import**.
3. Enter ID `9628` and click **Load**.
4. Select **Prometheus** as the data source.
5. Click **Import**.

**Key Metrics Tracked:**

* 🔴 **Slow Queries:** Top execution time and high-latency statements.
* 🟡 **Index Hit Rate:** Efficiency of memory usage vs. disk I/O.
* 🟢 **Active Connections:** Current sessions and potential locks.

---

## ⚙️ Configuration Details

### 1. PostgreSQL (Performance Tracking)

To enable deep query monitoring, the container is initialized with:

```sql
shared_preload_libraries = 'pg_stat_statements'
pg_stat_statements.track = all

```

### 2. Prometheus Scrape Config

The `prometheus.yml` is configured to pull data from the exporter every 15 seconds:

```yaml
scrape_configs:
  - job_name: 'postgres'
    static_configs:
      - targets: ['postgres-exporter:9187']

```

---

## 🛠️ Extensibility (Future Updates)

This stack is designed to be easily expanded:

* **[ ] Alerting:** Add `Alertmanager` to receive Slack/Email notifications for slow queries.
* **[ ] Logging:** Integrate `Grafana Loki` to correlate SQL logs with performance spikes.
* **[ ] Node Monitoring:** Add `node_exporter` to monitor the host machine's RAM/CPU.
* **[ ] Scaling:** Migrate `DATA_SOURCE_NAME` to Docker Secrets for production security.

---

## 📝 Troubleshooting

* **Empty Dashboard?** Ensure you ran `CREATE EXTENSION pg_stat_statements;` in your Postgres database.
* **Connection Refused?** Check if the `POSTGRES_DB` name in the `.env` or `docker-compose` matches the `DATA_SOURCE_NAME` in the exporter.

---

**Would you like me to add a specific section for Environment Variables or a Troubleshooting guide for common Docker networking issues?**