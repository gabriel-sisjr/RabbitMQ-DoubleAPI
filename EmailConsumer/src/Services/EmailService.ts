import { Channel } from "amqplib";
import nodemailer from "nodemailer";
import SMTPTransport from "nodemailer/lib/smtp-transport";
import { EmailInfo } from "../Interfaces/Auxs/Email";
import { QueueName } from "../Interfaces/Enums/QueueName";
import People from "../Interfaces/People";

let message: EmailInfo = {
  from: process.env.EMAIL_FROM || "from-example@email.com",
  to: "",
  subject: "People Inserted",
  text: "People inserted successfully",
};

const sendEmail = async (
  channel: Channel,
  transporter: nodemailer.Transporter<SMTPTransport.SentMessageInfo>
) => {
  await channel.assertQueue(QueueName.EMAIL);
  await channel.consume(QueueName.EMAIL, (msg) => {
    let parsed = JSON.parse(msg!.content.toString());

    if (Array.isArray(parsed)) {
      let peoples = parsed as People[];
      let emails = "";

      // concating emails to send
      // when sended this way, the receivers will see all the others emails
      // recommended is make a method and send one email for each receiver
      // if send a lot of emails at the same time, the connection will refuse cus is spam
      peoples.forEach((people) => (emails += `${people.Email},\n`));

      message.to = emails;
      send(message, transporter);
    } else {
      let people = parsed as People;
      message.to = people.Email;
      send(message, transporter);
    }

    channel.ack(msg!);
  });
};

const send = (
  message: EmailInfo,
  transporter: nodemailer.Transporter<SMTPTransport.SentMessageInfo>
) =>
  transporter
    .sendMail(message)
    .then((info) => console.log(info))
    .catch((err) => console.log(err));

export { sendEmail };
