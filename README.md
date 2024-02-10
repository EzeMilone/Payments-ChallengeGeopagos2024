# Payments-ChallengeGeopagos2024

## Requerimientos del proyecto
Estos requerimientos fueron estraidos del documento compartido por la organización Geopagos:

Challenge Payments - Geopagos - 2024.

Se requiere construir una solución que dé soporte a la autorización de pagos para nuestros clientes.

La solución constará de una API pública que será consumida por nuestros clientes donde recibiremos la solicitud de autorización y responderemos de acuerdo a lo que nos responda el procesador del pago.

El procesador de pago será un servicio independiente a la solución que simplemente nos responderá como aprobada la autorización cuando el monto sea un número entero y denegado si el monto contiene decimales.

Las autorizaciones pueden ser de cobro, devolución o reversa.

De acuerdo al tipo de cliente, se puede tener distintos modelos de autorización.
- Primero: Solicitud de autorización
- Segundo: Solicitud de autorización más su posterior confirmación.

Para aquellos clientes que cuenten con el segundo modelo se debe realizar una acción que genere una autorización de reversa de la solicitud generada en el paso anterior si la misma no fue confirmada dentro de los primeros cinco minutos.

Se deberá llevar un registro de todas las solicitudes de autorizaciones con su respectivo estado e información asociada.

Adicionalmente, debemos llevar otro registro que solo incluya las autorizaciones aprobadas (necesario para alimentar un proceso interno de reportería), este registro podrá ser solo una tabla que indique fecha, monto y cliente. Este registro deberá ser completado de manera asíncrona para no interferir en el proceso de autorización.

A fines prácticos podemos asumir que los clientes ya se encuentran registrados y ya se encuentra resuelta la autenticación.

Aclaraciones:
La solución debe poder ejecutarse localmente en un ambiente sin conexión a internet, se sugiere el uso de docker. De ser necesario dar indicaciones para su instalación y uso, incluirlas en un archivo readme.

Se valoran buenas prácticas, código limpio.
