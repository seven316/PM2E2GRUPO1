const {Router} = require("express")
const router = Router()

const admin = require("firebase-admin");
const db = admin.firestore();


router.post("/api/datos", async (req, res) => {
    try {
        await db
            .collection("datos")
            .doc("/" + req.body.id + "/")
            .create({descripcion: req.body.descripcion, latitud: req.body.latitud, longuitud: req.body.longuitud, fotografia: req.body.fotografia});
        return res.status(204).json();
    } catch (error) {
        console.log(error);
        return res.status(500).send(error);
    }
});


router.get("/api/datos/:product_id", (req, res) => {
    (async () => {
        try {
            const doc = db.collection("datos").doc(req.params.product_id);
            const item = await doc.get()
            const response = item.data()
            return res.status(200).json(response)
        } catch (error) {
            return res.status(500).send(error);
        }
    })();
});


router.get("/api/datos", async (res) => {
    try {
        const query = db.collection("datos");
        const querySnapshot = await query.get();
        const docs = querySnapshot.docs;

        const response = docs.map(doc => ({
            id: doc.id,
            descripcion: doc.data().descripcion,
            latitud: doc.data().latitud,
            longuitud: doc.data().longuitud,
            fotografia: doc.data().fotografia

        }));

        return res.status(200).json(response);
    } catch (error) {
        return res.status(500).json()
    }
});



router.delete("/api/datos/:product_id", async (req, res) => {
    try {
        const document = db.collection("datos").doc(req.params.product_id);
        await document.delete();
        return res.status(200).json();
    } catch (error) {
        return res.status(500).json();
    }
});

router.put("/api/datos/:product_id", async (req, res) => {
    try {
        const document = db.collection("datos").doc(req.params.product_id);
        await document.update({
            descripcion: req.body.descripcion,
            latitud: req.body.latitud,
            longuitud: req.body.longuitud,
            fotografia: req.body.fotografia
        });
        return res.status(200).json();
    } catch (error) {
        return res.status(500).json();
    }
});


module.exports = routerclear
