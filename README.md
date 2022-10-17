# GROUP6_CRIMSONSNACKS



add a flag col to user database that checks clock in or not, and that preserves clocked in or out
for the clock in/out bubtton


for approvals on time change req:
if multiple clock in/outs in one day we need to have an id for each clock in/out and need to set a time constraint on it (ie. every clock in for 2 weeks has an id, then on the req we need to send that id with it so when the admin approves it the system will know which one to change and update)


or, we could also have the user select the incorrect clock in/out times and send that with the req so the system knows what to update, 

i favor the second option so: 
incorrect clock in: 7:04 am
correct clock in 6:30 am
incorrect clock out: 4:00 pm
correct clock out: 3:45 pm


then the system could go into the database and alter the clock in and out times to be correct.